namespace RequestForm.Models
{
    public class NewEmployeeForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public List<Drive> Drives { get; set; }
        public string ManagerName { get; set; }
        public List<Software> Softwares { get; set; }
    }
}
