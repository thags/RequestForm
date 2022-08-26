using System;
using RequestForm.Interfaces;
using RequestForm.Models;

namespace RequestForm.Controllers
{
	public class DrivesController : DrivesInterface
	{
        IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public DrivesController()
		{

		}

        public List<Drive> GetDrives()
        {
            


            throw new NotImplementedException();
            
        }
    }
}

