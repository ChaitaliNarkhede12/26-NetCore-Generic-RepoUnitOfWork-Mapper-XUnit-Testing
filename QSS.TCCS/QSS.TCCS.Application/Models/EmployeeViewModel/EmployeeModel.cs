using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.Application.Models.EmployeeViewModel
{
    public class EmployeeModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string EmailId { get; set; }
		public string Gender { get; set; }
		public string MobileNumber { get; set; }
		public int? Salary { get; set; }
	}
}
