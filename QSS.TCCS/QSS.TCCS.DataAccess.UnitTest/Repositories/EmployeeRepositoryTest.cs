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

namespace QSS.TCCS.DataAccess.UnitTest.Repositories
{
    public class EmployeeRepositoryTest : IClassFixture<TCCSDataFixture>, IDisposable
    {
        TCCSDataFixture fixture;

        public EmployeeRepositoryTest(TCCSDataFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetAllEmployee_ShouldReturnList()
        {
            //Arrange
            var employeeList = GetEmployeeList();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee,int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = await employeeRepository.GetAllEmployee();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Employee>>(result);
            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnSpecificRecord()
        {
            //Arrange
            int id = 1;
            var employeeList = GetEmployeeList();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = await employeeRepository.GetEmployeeById(id);

            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEmployeeByIdUsingPredicate_ShouldReturnNullIfIdNotMatch()
        {
            //Arrange
            int id = 0;
            var employeeList = GetEmployeeList();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = await employeeRepository.GetEmployeeById(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEmployeeByIdUsingPredicate_ShouldReturnSpecificRecord()
        {
            //Arrange
            int id = 1;
            var employeeList = GetEmployeeList();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqDataList(employeeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = await employeeRepository.GetEmployeeById(x => x.Id == id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Employee>>(result);
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task AddEmploeeAsync_ShouldSaveEmployee()
        {
            //Arrange
            var employee = GetEmployee();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            var addEmployee = employee;
            addEmployee.Id = 0;

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = await employeeRepository.AddEmployeeAsync(addEmployee);

            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.Equal(employee.Name, result.Name);
        }

        [Fact]
        public async Task AddProductAsync_ThrowException()
        {
            //Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            Task act() => employeeRepository.AddEmployeeAsync(null);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }


        [Fact]
        public void UpdateEmployee_ShouldUpdateEmployee()
        {
            //Arrange
            var employee = GetEmployee();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);
            employee.Name = "test111";

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            var result = employeeRepository.UpdateEmployee(employee);

            //Assert
            Assert.IsAssignableFrom<Employee>(result);
            Assert.Equal(employee.Name, result.Name);
        }

        [Fact]
        public void UpdateEmployee_ShouldThrowException()
        {
            //Arrange
            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            Employee act() => employeeRepository.UpdateEmployee(null);

            //Assert
            Assert.Throws<NullReferenceException>(act);
        }

        [Fact]
        public void RemoveEmployee_ShouldRemoveEmployee()
        {
            //Arrange
            var employee = GetEmployee();
            employee.Id = 5;
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            employeeRepository.RemoveEmployee(employee);
        }

        [Fact]
        public async Task RemoveEmployeeById_ShouldRemoveEmployeeAsync()
        {
            //Arrange
            int id = 1;
            var employee = GetEmployee();
            EmployeeMoq employeeMoq = new EmployeeMoq(fixture);
            employeeMoq.MoqData(employee);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.tccsContext);
            IRepository<Employee, int> repository = new Repository<Employee, int>(unitOfWork);
            IEmployeeRepository employeeRepository = new EmployeeRepository(repository);

            //Act
            await employeeRepository.RemoveEmployeeById(id);
        }

        public void Dispose()
        {
            fixture.tccsContext.Database.EnsureDeleted();
        }




        private IEnumerable<Employee> GetEmployeeList()
        {
            List<Employee> employeeList = new List<Employee>()
            {
                new Employee{Id=1,Name="test1",EmailId="test1@gmail.com",Gender="Male",MobileNumber="1234567890",Salary=30000},
                new Employee{Id=2,Name="test2",EmailId="test2@gmail.com",Gender="Female",MobileNumber="1234567890",Salary=20000},
                new Employee{Id=3,Name="test3",EmailId="test3@gmail.com",Gender="Female",MobileNumber="1234567890",Salary=80000},
                new Employee{Id=4,Name="test4",EmailId="test4@gmail.com",Gender="Male",MobileNumber="1234567890",Salary=50000},
            };

            return employeeList;
        }

        private Employee GetEmployee()
        {
            Employee employee = new Employee()
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
    }
}
