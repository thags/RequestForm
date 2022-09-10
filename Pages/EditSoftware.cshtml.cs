using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RequestForm.Controllers;
using RequestForm.Interfaces;
using RequestForm.Models;

namespace RequestForm.Pages;

public class EditSoftwareModel : PageModel
{
    private readonly DBInterface db = new sqliteController();
    public List<Software> software = new List<Software>();


    public void OnGet()
    {
        GetSoftware();
    }

    public void GetSoftware()
    {
        software.AddRange(db.GetAllSoftware());
        software.Add(
            new Software()
            {
                Id = -1,
            });
        software.Reverse();
        
    }

    public IActionResult OnPost(IFormCollection form)
    {
        GetSoftware();
        int index = int.Parse(form.ToList()[0].Value);
        Software? softwareEditing = software.Find(x => x.Id == index);

        if (softwareEditing == null)
        {
            return RedirectToPage("./EditSoftware");
        }

        softwareEditing.SoftwareName = form.ToList()[1].Value;

        if (String.IsNullOrEmpty(softwareEditing.SoftwareName))
        {
            db.DeleteSoftware(softwareEditing.Id);
            return RedirectToPage("./EditSoftware");
        }
        
        if (softwareEditing.Id == -1)
        {
            db.AddSoftware(softwareEditing);
            return RedirectToPage("./EditSoftware");
        }

        db.EditSoftware(softwareEditing);

        return RedirectToPage("./EditSoftware");
    }

}


