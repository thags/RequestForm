using System.ComponentModel.DataAnnotations;

namespace RequestForm.Models
{
    public class NewEmployeeForm
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        public List<Drive> Drives { get; set; }
        [Required]
        public string ManagerName { get; set; }
        public List<Software> Softwares { get; set; }
    }
}
