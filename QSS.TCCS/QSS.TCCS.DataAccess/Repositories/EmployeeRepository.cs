
using QSS.TCCS.DataAccess.DBContext;
using QSS.TCCS.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QSS.TCCS.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        IRepository<Employee, int> _repository;
        public EmployeeRepository(IRepository<Employee, int> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return (IEnumerable<Employee>)await _repository.GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeById(Expression<Func<Employee, bool>> predicate)
        {
            return await _repository.GetById(predicate);
        }

        public async Task<Employee> AddEmployeeAsync(Employee entity)
        {
            return await _repository.AddAsync(entity);
        }

        public Employee UpdateEmployee(Employee entity)
        {
            return _repository.Update(entity);
        }

        public void RemoveEmployee(Employee entity)
        {
            _repository.Remove(entity);
        }

        public async Task RemoveEmployeeById(int id)
        {
            await _repository.RemoveById(id);
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }


        public async Task<Employee> SingleOrDefaultEmployee(Expression<Func<Employee, bool>> predicate)
        {
            return await _repository.SingleOrDefault(predicate);
        }

        public async Task<Employee> FirstOrDefaultEmployee(Expression<Func<Employee, bool>> predicate)
        {
            return await _repository.FirstOrDefault(predicate);
        }

        public void AddEmployeeRange(IEnumerable<Employee> entities)
        {
            _repository.AddRange(entities);
        }

        public async Task AddEmployeeRangeAsync(IEnumerable<Employee> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public void UpdateEmployeeRange(IEnumerable<Employee> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void RemoveEmployeeRange(IEnumerable<Employee> entities)
        {
            _repository.RemoveRange(entities);
        }
    }
}
