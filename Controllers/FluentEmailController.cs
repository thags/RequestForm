using RequestForm.Interfaces;
using RequestForm.Models;
using System.Net.Mail;
using System.Net;
using FluentEmail.Smtp;
using FluentEmail.Core.Models;
using FluentEmail.Core;

namespace RequestForm.Controllers
{
    public class FluentEmailController : EmailInterface
    {
        public bool SendEmail(string emailBody)
        {
            if (emailBody == null) return false;

            SendResponse emailResponse;
            try
            {
                var emailInfo = GetEmailConfiguration();
                Email.DefaultSender = GetSmtpSenderFromConfig(emailInfo);
                var email = Email
                    .From(emailInfo.EmailFrom)
                    .To(emailInfo.EmailTo, emailInfo.EmailToNickName)
                    .Subject(emailInfo.EmailSubject)
                    .Body(emailBody);

                emailResponse = email.SendAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return false;
            }

            if (emailResponse.Successful) return true;

            return false;
        }

        private SmtpSender GetSmtpSenderFromConfig(EmailSMTPConfig emailConfig)
        {
            return new SmtpSender(() => new SmtpClient(emailConfig.Server)
            {
                UseDefaultCredentials = false,
                Port = emailConfig.Port,
                Credentials = new NetworkCredential(emailConfig.Username, emailConfig.Password),
                EnableSsl = emailConfig.EnableSSL,
            });
        }

        private EmailSMTPConfig GetEmailConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var section = config.GetSection(nameof(EmailSMTPConfig));
            return section.Get<EmailSMTPConfig>();
        }
    }
}
