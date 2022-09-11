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
    private readonly IDBInterface db;
    private IEmailInterface emailController;

    public NewEmployeeForm employeeInfo = new NewEmployeeForm();
    public string? emailStatusMessage = null;
    public string? emailAlertClass = null;

    public IndexModel(ILogger<IndexModel> logger, IDBInterface dbController, IEmailInterface email)
    {
        _logger = logger;
        db = dbController;
        emailController = email;
    }

    public void OnGet()
    {
        LoadDBInfo();
    }

    public IActionResult OnPost()
    {
        emailStatusMessage = null;
        emailAlertClass = null;

        string emailBody = BuildEmail(Request.Form);
        bool emailSent = SendEmail(emailBody);

        LoadDBInfo();

        if(emailSent == false)
        {
            emailAlertClass = "alert alert-warning";
            emailStatusMessage = @"Failed to send request, please try again or contact your adminsitrator!";
        }

        if(emailSent == true)
        {
            emailAlertClass = "alert alert-success";
            emailStatusMessage = @"Succesfully sent request";
        }

        return Page();
    }

    private void LoadDBInfo()
    {
        employeeInfo.Drives = db.GetAllDrives();
        employeeInfo.Softwares = db.GetAllSoftware();
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


