using System;
using System.Collections.Generic;

#nullable disable

namespace QSS.TCCS.DataAccess.DBContext
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public int? Salary { get; set; }
    }
}
