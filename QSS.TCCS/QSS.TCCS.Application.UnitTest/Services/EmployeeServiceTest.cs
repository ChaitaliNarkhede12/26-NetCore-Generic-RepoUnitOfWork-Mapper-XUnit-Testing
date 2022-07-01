using QSS.TCCS.Application.Interfaces;
using QSS.TCCS.Application.Models.EmployeeViewModel;
using QSS.TCCS.Application.Services;
using QSS.TCCS.DataAccess.DBContext;
using QSS.TCCS.DataAccess.Interfaces;
using QSS.TCCS.DataAccess.Repositories;
using QSS.TCCS.UnitTest.Core;
using QSS.TCCS.UnitTest.Core.MoqData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QSS.TCCS.Application.UnitTest.Services
{
    public class EmployeeServiceTest : IClassFixture<TCCSDataFixture>, IDisposable
    {
        TCCSDataFixture fixture;

        public EmployeeServiceTest(TCCSDataFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetAllEmployee_ShouldReturnList()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModelList = GetEmployeeModelList();
            var employeeList = mapper.Map<IEnumerable<Employee>>(employeeModelList);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository,mapper);

            //Act
            var result = await service.GetAllEmployee();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<EmployeeModel>>(result);
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModelList = GetEmployeeModelList();
            var employeeList = mapper.Map<IEnumerable<Employee>>(employeeModelList);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.GetEmployeeById(id);

            //Assert
            Assert.IsAssignableFrom<EmployeeModel>(result);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEmployeeByIdUsingPredicate_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModelList = GetEmployeeModelList();
            var employeeList = mapper.Map<IEnumerable<Employee>>(employeeModelList);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.GetEmployeeById(x=>x.Id == id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<EmployeeModel>>(result);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldSaveEmployee()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModel = GetEmployeeModel();
            var employee = mapper.Map<Employee>(employeeModel);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            var addEmployee = AddEmployeeModel();
            addEmployee.Id = 0;

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.AddEmployeeAsync(addEmployee);

            //Assert
            Assert.IsAssignableFrom<EmployeeModel>(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldthrowException()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            Task act() => service.AddEmployeeAsync(null);

            //Assert
            await Assert.ThrowsAsync<System.Exception>(act);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldUpdateEmployee()
        {
            //Arrange
            string updatedValue = "test1111";
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModel = GetEmployeeModel();
            var employee = mapper.Map<Employee>(employeeModel);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            employeeModel.Name = updatedValue;

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.UpdateEmployee(employeeModel);

            //Assert
            Assert.IsAssignableFrom<EmployeeModel>(result);
            Assert.Equal(updatedValue, result.Name);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldthrowException()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            Task act() => service.UpdateEmployee(null);

            //Assert
            await Assert.ThrowsAsync<System.Exception>(act);
        }

        [Fact]
        public async Task RemoveEmployee_ShouldRemoveEmployee()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModel = GetEmployeeModel();
            employeeModel.Id = 5;
            var employee = mapper.Map<Employee>(employeeModel);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.RemoveEmployee(employeeModel);

            //Assert
            Assert.IsAssignableFrom<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task RemoveEmployee_ShouldThrowExceptionWhenWePassNull()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            Task act() => service.RemoveEmployee(null);

            //Assert
            await Assert.ThrowsAsync<System.Exception>(act);
        }

        [Fact]
        public async Task RemoveEmployeeById_ShouldRemoveEmployee()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModel = GetEmployeeModel();
            var employee = mapper.Map<Employee>(employeeModel);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            var result = await service.RemoveEmployeeById(id);

            //Assert
            Assert.IsAssignableFrom<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task RemoveProductById_ShouldThrowExceptionWhenIdNotMatch()
        {
            //Arrange
            int id = 0;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new EmployeeMapping());

            var employeeModel = GetEmployeeModel();
            var employee = mapper.Map<Employee>(employeeModel);

            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);
            IEmployeeService service = new EmployeeService(employeeRepository, mapper);

            //Act
            Task act() => service.RemoveEmployeeById(id);

            //Assert
            await Assert.ThrowsAsync<System.Exception>(act);
        }



        public void Dispose()
        {
            fixture.tccsContext.Database.EnsureDeleted();
        }


        private IEnumerable<EmployeeModel> GetEmployeeModelList()
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>()
            {
                new EmployeeModel{Id=1,Name="test1",EmailId="test1@gmail.com",Gender="Male",MobileNumber="1234567890",Salary=30000},
                new EmployeeModel{Id=2,Name="test2",EmailId="test2@gmail.com",Gender="Female",MobileNumber="1234567890",Salary=20000},
                new EmployeeModel{Id=3,Name="test3",EmailId="test3@gmail.com",Gender="Female",MobileNumber="1234567890",Salary=80000},
                new EmployeeModel{Id=4,Name="test4",EmailId="test4@gmail.com",Gender="Male",MobileNumber="1234567890",Salary=50000},
            };

            return employeeList;
        }

        private EmployeeModel GetEmployeeModel()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                Id = 1,
                Name = "test1",
                EmailId = "test1@gmail.com",
                Gender = "Male",
                MobileNumber = "1234567890",
                Salary = 30000
            };

            return employee;
        }

        private EmployeeModel AddEmployeeModel()
        {
            EmployeeModel employee = new EmployeeModel()
            {
                Id = 0,
                Name = "abc",
                EmailId = "abc@gmail.com",
                Gender = "Male",
                MobileNumber = "1234567890",
                Salary = 30000
            };

            return employee;
        }
    }
}
