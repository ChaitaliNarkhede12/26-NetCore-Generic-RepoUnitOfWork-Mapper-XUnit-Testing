using QSS.TCCS.DataAccess.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QSS.TCCS.UnitTest.Core.MoqData
{
    public class EmployeeMoq : IClassFixture<TCCSDataFixture>
    {
        TCCSDataFixture fixture;

        public EmployeeMoq(TCCSDataFixture fixture)
        {
            this.fixture = fixture;
        }
        public void MoqData(Employee entity)
        {
            using (var qssContext = new TCCSContext(fixture.tccsContextOptions))
            {
                qssContext.Employees.Add(entity);
                qssContext.SaveChanges();
            }
        }

        public void MoqDataList(IEnumerable<Employee> entityList)
        {
            using (var qssContext = new TCCSContext(fixture.tccsContextOptions))
            {
                qssContext.Employees.AddRangeAsync(entityList);
                qssContext.SaveChanges();
            }
        }
    }
}
