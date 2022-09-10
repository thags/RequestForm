using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestForm.Controllers;
using RequestForm.Interfaces;
using RequestForm.Models;

namespace RequestForm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DBInterface db = new sqliteController();
    public NewEmployeeForm employeeInfo = new NewEmployeeForm();
    private EmailInterface emailController = new FluentEmailController();

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
        SendEmail(emailBody);
        return RedirectToPage("./Index");
    }

    private string BuildEmail(IFormCollection form)
    {
        if (form == null) return String.Empty;

        StringBuilder drives = new StringBuilder();
        StringBuilder software = new StringBuilder();

        if (form["drive_checked"].Count > 0)
        {
            drives.AppendLine("Drives: ");
            foreach(string drive in form["drive_checked"])
            {
                drives.AppendLine("- " + drive);
            }
        }

        if (form["software_checked"].Count > 0)
        {
            software.AppendLine("Software: ");
            foreach (string sw in form["software_checked"])
            {
                software.AppendLine("- " + sw);
            }
        }

        return $"User: {form["FirstName"]} {form["LastName"]}\r\nJob Title: {form["JobTitle"]}\r\nManager: {form["Manager"]}\r\n\r\n{drives.ToString()}\r\n{software.ToString()}";
    }

    private bool SendEmail(string emailBody)
    {
        return emailController.SendEmail(emailBody);
    }
}


