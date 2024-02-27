using CarModelMgmt.Core.Interfaces;
using CarModelMgmt.Infrastructure;
using CarModelMgmt.Infrastructure.Repositories;
using CarModelMgmt.Services.Configuration;
using CarModelMgmt.Services.Helpers;
using CarModelMgmt.Services.Interfaces;
using CarModelMgmt.Services.Services;
using CarModelMgmt.WebApp.Helpers;
using CarModelMgmt.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



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
builder.Services.AddScoped<IMenuService, MenuService>();
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

// Add authorization options (if needed)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrganiserPolicy", policy => policy.RequireRole("Organiser"));
    options.AddPolicy("VoterPolicy", policy => policy.RequireRole("Voter"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
