using AutoMapper;
using QSS.TCCS.Application.Interfaces;
using QSS.TCCS.Application.Models.EmployeeViewModel;
using QSS.TCCS.DataAccess.DBContext;
using QSS.TCCS.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QSS.TCCS.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployee()
        {
            try
            {
                IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployee();
                return _mapper.Map<IEnumerable<EmployeeModel>>(employees);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> GetEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employeeRepository.GetEmployeeById(id);
                return _mapper.Map<EmployeeModel>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeeById(Expression<Func<Employee, bool>> predicate)
        {
            try
            {
                IEnumerable<Employee> employee = await _employeeRepository.GetEmployeeById(predicate);
                return _mapper.Map<IEnumerable<EmployeeModel>>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel entity)
        {
            try
            {
                var employee = _mapper.Map<Employee>(entity);
                var data = await _employeeRepository.AddEmployeeAsync(employee);

                int result = await _employeeRepository.SaveChangesAsync();
                return _mapper.Map<EmployeeModel>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EmployeeModel> UpdateEmployee(EmployeeModel entity)
        {
            try
            {
                var employee = _mapper.Map<Employee>(entity);
                var data = _employeeRepository.UpdateEmployee(employee);

                int result = await _employeeRepository.SaveChangesAsync();
                return _mapper.Map<EmployeeModel>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveEmployee(EmployeeModel entity)
        {
            try
            {
                var employee = _mapper.Map<Employee>(entity);
                _employeeRepository.RemoveEmployee(employee);

                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveEmployeeById(int id)
        {
            try
            {
                await _employeeRepository.RemoveEmployeeById(id);
                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<EmployeeModel> SingleOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate)
        {
            try
            {
                Employee employee = await _employeeRepository.SingleOrDefaultEmployee(predicate);
                return _mapper.Map<EmployeeModel>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EmployeeModel> FirstOrDefaultEmployeeAsync(Expression<Func<Employee, bool>> predicate)
        {
            try
            {
                var employee = await _employeeRepository.FirstOrDefaultEmployee(predicate);
                return _mapper.Map<EmployeeModel>(employee);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddEmployeeRange(IEnumerable<EmployeeModel> entities)
        {
            try
            {
                var employees = _mapper.Map<IEnumerable<Employee>>(entities);
                _employeeRepository.AddEmployeeRange(employees);

                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> AddEmployeeRangeAsync(IEnumerable<EmployeeModel> entities)
        {
            try
            {
               var employees = _mapper.Map<IEnumerable<Employee>>(entities);
                await _employeeRepository.AddEmployeeRangeAsync(employees);

                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateEmployeeRange(IEnumerable<EmployeeModel> entities)
        {
            try
            {
                var employees = _mapper.Map<IEnumerable<Employee>>(entities);
                _employeeRepository.UpdateEmployeeRange(employees);

                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveEmployeeRange(IEnumerable<EmployeeModel> entities)
        {
            try
            {
                var employees = this._mapper.Map<IEnumerable<Employee>>(entities);
                this._employeeRepository.RemoveEmployeeRange(employees);

                int result = await _employeeRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
