using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security;

namespace RequestForm.Models
{
    public class EmailSMTPConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string EmailTo { get; set; }
        public string EmailToNickName  { get; set; }
        public string EmailFrom { get; set; }
        public string EmailSubject { get; set; }

    }
}
