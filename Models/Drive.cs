using System;
namespace RequestForm.Models
{
	public class Drive
	{
		public int Id { get; set; }
		public string DriveLetter { get; set; }
		public string DriveName { get; set; }
		public bool IsChecked { get; set; }
	}
}

