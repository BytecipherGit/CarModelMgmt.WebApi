using CarModelMgmt.Core.Interfaces;
using CarModelMgmt.Infrastructure;
using CarModelMgmt.Infrastructure.Repositories;
using CarModelMgmt.Services.Configuration;
using CarModelMgmt.Services.Helpers;
using CarModelMgmt.Services.Interfaces;
using CarModelMgmt.WebApi.Helpers;
using CarModelMgmt.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ConfigurationService>();
// Register the Dapper connection
string connectionString = builder.Configuration.GetConnectionString("PollVoteDatabase");
builder.Services.AddScoped<DapperHelper>(_ => new DapperHelper(connectionString));


// Register the specific repository and service for the Poll entity
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGridSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EncryptionHelper>();

// Register the SendGrid client
builder.Services.AddSingleton<ISendGridClient>(provider =>
{
    var sendGridSettings = provider.GetRequiredService<IOptions<SendGridSettings>>().Value;
    return new SendGridClient(sendGridSettings.ApiKey);
});




builder.Services.AddScoped<PasswordGenerator>();

// Register the PasswordHasher class in the DI container
builder.Services.AddScoped<Hasher>();

// Configure AutoMapper


// Read the secret key from appsettings.json
string secretKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
int tokenExpirationInMinutes = int.Parse(builder.Configuration.GetSection("JwtSettings:TokenExpirationInMinutes").Value);
var key = Encoding.ASCII.GetBytes(secretKey);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://app.pollvote.org")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Add authorization options (if needed)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganiserPolicy", policy => policy.RequireRole("Organiser"));
    options.AddPolicy("VoterPolicy", policy => policy.RequireRole("Voter"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<JwtHelper>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    return new JwtHelper(secretKey, tokenExpirationInMinutes, httpContextAccessor);
});

// Add other services to the container
builder.Services.AddControllers();

// Configure custom validation error response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.ToDictionary(
            entry => entry.Key,
            entry => entry.Value.Errors.Select(error => error.ErrorMessage).ToArray()
        );

        var apiResponse = new ApiResponse<object>(false, null, errors.FirstOrDefault().Value.FirstOrDefault(), 400);
        return new BadRequestObjectResult(apiResponse);
    };
});

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Pollvote API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
