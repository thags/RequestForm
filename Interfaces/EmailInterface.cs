using RequestForm.Models;

namespace RequestForm.Interfaces
{
    public interface EmailInterface
    {
        bool SendEmail(string emailBody);

    }
}
