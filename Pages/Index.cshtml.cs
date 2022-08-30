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
}


