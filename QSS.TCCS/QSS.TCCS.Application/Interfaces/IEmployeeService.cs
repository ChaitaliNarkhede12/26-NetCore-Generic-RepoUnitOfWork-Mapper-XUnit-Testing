using QSS.TCCS.Application.Models.EmployeeViewModel;
using QSS.TCCS.DataAccess.DBContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QSS.TCCS.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAllEmployee();
        Task<EmployeeModel> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeModel>> GetEmployeeById(Expression<Func<Employee, bool>> predicate);
        Task<EmployeeModel> AddEmployeeAsync(EmployeeModel entity);
        Task<EmployeeModel> UpdateEmployee(EmployeeModel entity);
        Task<int> RemoveEmployee(EmployeeModel entity);
        Task<int> RemoveEmployeeById(int id);

        
        Task<EmployeeModel> SingleOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate);
        Task<EmployeeModel> FirstOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate);
        
        Task<int> AddEmployeeRange(IEnumerable<EmployeeModel> entities);
        Task<int> AddEmployeeRangeAsync(IEnumerable<EmployeeModel> entities);
        
        Task<int> UpdateEmployeeRange(IEnumerable<EmployeeModel> entities);

        Task<int> RemoveEmployeeRange(IEnumerable<EmployeeModel> entities);
    }
}
