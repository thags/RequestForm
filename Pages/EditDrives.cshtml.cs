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
        GetDrives();
    }

    public void GetDrives()
    {
        drives = db.GetAllDrives();
        drives.Add(
            new Drive()
            {
                Id = -1,
            });
    }

    public IActionResult OnPost(IFormCollection form)
    {
        GetDrives();
        int index = int.Parse(form.ToList()[0].Value);
        Drive? driveEditing = drives.Find(x => x.Id == index);

        if (driveEditing == null)
        {
            return RedirectToPage("./EditDrives");
        }

        driveEditing.DriveLetter = form.ToList()[1].Value;
        driveEditing.DriveName = form.ToList()[2].Value;

        if (String.IsNullOrEmpty(driveEditing.DriveName) && String.IsNullOrEmpty(driveEditing.DriveLetter))
        {
            db.DeleteDrive(driveEditing.Id);
            return RedirectToPage("./EditDrives");
        }
        
        if (driveEditing.Id == -1)
        {
            db.AddDrive(driveEditing);
            return RedirectToPage("./EditDrives");
        }

        db.EditDrive(driveEditing);

        return RedirectToPage("./EditDrives");
    }

}


