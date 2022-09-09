using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestForm.Controllers;
using RequestForm.Interfaces;
using RequestForm.Models;

namespace RequestForm.Pages;

public class EditDrivesModel : PageModel
{
    private readonly DBInterface db = new sqliteController();
    public List<Drive> drives = new List<Drive>();


    public void OnGet()
    {
        drives = db.GetAllDrives();
    }
    
}


