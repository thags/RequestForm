using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestForm.Controllers;
using RequestForm.Interfaces;
using RequestForm.Models;

namespace RequestForm.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private DBInterface db = new sqliteController();
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

}

