using System;
using RequestForm.Models;
namespace RequestForm.Interfaces
{
	public interface DBInterface
	{
		public void CreateTables();
		public void AddDrive(Drive newDrive);
		public void EditDrive(Drive editedDrive);
		public List<Drive> GetAllDrives();
		public void DeleteDrive(int id);
		public void AddSoftware(Software newSoftware);
		public void EditSoftware(Software editedSoftware);
		public void DeleteSoftware(int id);
		public List<Software> GetAllSoftware();
	}
}

