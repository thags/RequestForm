using System.Text;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorLight.Extensions;
using RequestForm.Controllers;
using RequestForm.Interfaces;
using RequestForm.Models;
using FluentEmail;
using System.Net.Mail;
using System.Net;

namespace RequestForm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DBInterface db = new sqliteController();
    public NewEmployeeForm employeeInfo = new NewEmployeeForm();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        employeeInfo.Drives = db.GetAllDrives();
        employeeInfo.Softwares = db.GetAllSoftware();
    }

    public IActionResult OnPost()
    {
        string emailBody = BuildEmail(Request.Form);
        SendEmail();
        return RedirectToPage("./Index");
    }

    private string BuildEmail(IFormCollection form)
    {
        if (form == null) return String.Empty;

        StringBuilder drives = new StringBuilder();
        StringBuilder software = new StringBuilder();

        if (form["drive_checked"].Count > 0)
        {
            foreach(string drive in form["drive_checked"])
            {
                drives.Append(drive);
            }
        }

        if (form["drive_checked"].Count > 0)
        {
            foreach (string drive in form["software_checked"])
            {
                software.Append(drive);
            }
        }

        return "Drives: "+drives.ToString() + " Software: " + software.ToString();
    }

    private bool SendEmail()
    {
        SendResponse emailResponse;
        try
        {
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("email@email.com", "password"),
                EnableSsl = true,
            });

            Email.DefaultSender = sender;
            var email = Email
                .From("Test@test.com")
                .To("email", "name")
                .Subject("test email")
                .Body("Test email body");

            emailResponse = email.SendAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            return false;
        }

        if (emailResponse.Successful) return true;

        return false;
    }
}


