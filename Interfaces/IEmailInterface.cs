using RequestForm.Models;

namespace RequestForm.Interfaces
{
    public interface IEmailInterface
    {
        bool SendEmail(string emailBody);

    }
}
