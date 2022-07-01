using Microsoft.AspNetCore.Mvc;
using Moq;
using QSS.TCCS.Api.Controllers;
using QSS.TCCS.Application.Interfaces;
using QSS.TCCS.Application.Models.EmployeeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QSS.TCCS.Api.UnitTest.Controller
{
    public class EmployeeControllerTest
    {
        [Fact]
        public async Task GetAllEmployee_ReturnOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.GetAllEmployee()).ReturnsAsync(GetEmployeeModelList());

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetAllEmployee();

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.GetEmployeeById(id)).ReturnsAsync(GetEmployeeModelById(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeById(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeById_ReturnNoContent()
        {
            // Arrange
            int id = 10;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.GetEmployeeById(id)).ReturnsAsync(GetEmployeeModelById(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeById(id);

            //// Assert
            Assert.Equal(204, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetEmployeeByIdUsingPredicate_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.GetEmployeeById(x => x.Id == id)).ReturnsAsync(GetEmployeeModelUsingPredicate(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeByIdUsingPredicate(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeSignleOrDefault_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.SingleOrDefaultEmployeeAsync(x => x.Id == id)).ReturnsAsync(GetEmployeeSingleOrDefault(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeSignleOrDefault(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeSignleOrDefault_ReturnNoContent()
        {
            // Arrange
            int id = 10;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.SingleOrDefaultEmployeeAsync(x => x.Id == id)).ReturnsAsync(GetEmployeeSingleOrDefault(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeSignleOrDefault(id);

            //// Assert
            Assert.Equal(204, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetEmployeeFirstOrDefault_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.FirstOrDefaultEmployeeAsync(x => x.Id == id)).ReturnsAsync(GetEmployeeFirstOrDerfault(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeFirstOrDefault(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetEmployeeFirstOrDefault_ReturnNoContent()
        {
            // Arrange
            int id = 10;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.FirstOrDefaultEmployeeAsync(x => x.Id == id)).ReturnsAsync(GetEmployeeFirstOrDerfault(id));

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.GetEmployeeFirstOrDefault(id);

            //// Assert
            Assert.Equal(204, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ReturnOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeService>();
            var addProduct = AddEmployeeModel();

            mockRepo.Setup(repo => repo.AddEmployeeAsync(addProduct)).ReturnsAsync(addProduct);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeAsync(addProduct);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeAsync(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddEmployeeRange_ReturnOkResult()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var addProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.AddEmployeeRange(addProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeRange(addProductList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddEmployeeRange_ReturnBadRequest()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var addProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.AddEmployeeRange(addProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task AddEmployeeRangeAsync_ReturnOkResult()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var addProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.AddEmployeeRangeAsync(addProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeRangeAsync(addProductList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddEmployeeRangeAsync_ReturnBadRequest()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var addProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.AddEmployeeRangeAsync(addProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.AddEmployeeRangeAsync(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var updateProduct = GetEmployeeModelById(id);

            mockRepo.Setup(repo => repo.UpdateEmployee(updateProduct)).ReturnsAsync(updateProduct);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateEmployee(updateProduct);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateEmployee(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeRange_ReturnOkResult()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var updateProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.UpdateEmployeeRange(updateProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateEmployeeRange(updateProductList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmployeeRange_ReturnBadRequest()
        {
            // Arrange
            int output = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var updateProductList = GetEmployeeModelList();

            mockRepo.Setup(repo => repo.UpdateEmployeeRange(updateProductList)).ReturnsAsync(output);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateEmployeeRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task RemoveEmployeeAsync_ReturnOkResultWithSucess()
        {
            // Arrange
            int id = 1;
            int returnData = 1;
            var mockRepo = new Mock<IEmployeeService>();
            var employee = GetEmployeeModelById(id);

            mockRepo.Setup(repo => repo.RemoveEmployee(employee)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployee(employee);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveEmployeeAsync_ReturnOkResultWithNotSucess()
        {
            // Arrange
            int id = 1;
            int returnData = 0;
            var mockRepo = new Mock<IEmployeeService>();
            var employee = GetEmployeeModelById(id);

            mockRepo.Setup(repo => repo.RemoveEmployee(employee)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployee(employee);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveEmployeeAsync_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployee(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task RemoveProductById_ReturnOkResultWithSucess()
        {
            // Arrange
            int returnData = 1;
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.RemoveEmployeeById(id)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeById(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveProductById_ReturnOkResultWithNotSucess()
        {
            // Arrange
            int returnData = 0;
            int id = 1;
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.RemoveEmployeeById(id)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeById(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveProductById_ReturnBadRequest()
        {
            // Arrange
            int id = 0;
            var mockRepo = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeById(id);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task RemoveEmployeeByRange_ReturnOkResultWithSucess()
        {
            // Arrange
            int returnData = 1;

            var employeeList = GetEmployeeModelList();
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.RemoveEmployeeRange(employeeList)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeByRange(employeeList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveEmployeeByRange_ReturnOkResultWithNotSucess()
        {
            // Arrange
            int returnData = 0;

            var employeeList = GetEmployeeModelList();
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.RemoveEmployeeRange(employeeList)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeByRange(employeeList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value);
        }

        [Fact]
        public async Task RemoveEmployeeByRange_ReturnBadRequest()
        {
            // Arrange
            int returnData = 0;

            var employeeList = GetEmployeeModelList();
            var mockRepo = new Mock<IEmployeeService>();

            mockRepo.Setup(repo => repo.RemoveEmployeeRange(employeeList)).ReturnsAsync(returnData);

            var controller = new EmployeeController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveEmployeeByRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
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

        private EmployeeModel GetEmployeeModelById(int emplyeeId)
        {
            var employee = GetEmployeeModelList().FirstOrDefault(x => x.Id == emplyeeId); ;
            return employee;
        }

        private IEnumerable<EmployeeModel> GetEmployeeModelUsingPredicate(int employeeId)
        {
            var productList = GetEmployeeModelList().Where(x => x.Id == employeeId);
            return productList;
        }

        private EmployeeModel GetEmployeeFirstOrDerfault(int employeeId)
        {
            var employee = GetEmployeeModelList().FirstOrDefault(x => x.Id == employeeId);
            return employee;
        }

        private EmployeeModel GetEmployeeSingleOrDefault(int employeeId)
        {
            var employee = GetEmployeeModelList().SingleOrDefault(x => x.Id == employeeId);
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
