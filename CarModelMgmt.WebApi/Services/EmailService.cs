using CarModelMgmt.Services.Configuration;
using CarModelMgmt.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace CarModelMgmt.WebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient sendGridClient;
        private readonly string fromEmail;

        public EmailService(ISendGridClient sendGridClient, IOptions<SendGridSettings> sendGridSettings)
        {
            this.sendGridClient = sendGridClient;
            fromEmail = sendGridSettings.Value.SenderEmail;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                var message = MailHelper.CreateSingleEmail(
                new EmailAddress(fromEmail),
                new EmailAddress(toEmail),
                subject,
                plainTextContent: null,
                htmlContent);

                await sendGridClient.SendEmailAsync(message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task SendPasswordResetEmailAsync(string email, string resetToken)
        {
            throw new NotImplementedException();
        }
    }
}
