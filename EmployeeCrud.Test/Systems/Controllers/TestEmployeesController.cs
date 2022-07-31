using EmployeeCrud.Domain;
using EmployeeCrud.Test.MockData;
using EmployeeCrud.Web.Controllers;
using EmployeeCrud.Web.Models.DTO;
using EmployeeCrud.Web.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeCrud.Test.Systems.Controllers
{
    public class TestEmployeesController
    {
        private readonly IEmployeeService _service;

        public TestEmployeesController()
        {
            _service = new EmployeeServiceMockData();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ShouldReturn200Status()
        {
            /// Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);

            /// Act
            var result = (OkObjectResult)await sut.GetAll(cancelToken);

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllEmpty_ShouldReturn200Status()
        {
            /// Arrange
            var todoService = new Mock<IEmployeeService>();
            var cancelToken = CancellationToken.None;
            todoService.Setup(e => e.GetAll(cancelToken)).ReturnsAsync(EmployeeMockData.GetAllEmpty());
            var sut = new EmployeesController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetAll();

            /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAll_ShouldReturnsAllItems()
        {
            /// Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);

            // Act
            var okResult = await sut.GetAll(cancelToken) as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Employee>>(okResult?.Value);
            Assert.Equal(3, items.Count);
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            /// Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);

            // Act
            var badResponse = await sut.Get(99999, cancelToken);
          
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            /// Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);

            // Act
            var okResult = await sut.Get(1, cancelToken);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        #endregion

        #region Create
        [Fact]
        public async void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            EmployeeDTO testItem = new EmployeeDTO()
            {
                FirstName = "tester",
                LastName = "ginger",
                Email = "gingin@gmail.com"
            };

            // Act
            var createdResponse = await sut.Create(testItem, cancelToken);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public async void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            EmployeeDTO testItem = new EmployeeDTO()
            {
                LastName = "ginger",
                Email = "gingin@gmail.com"
            };

            sut.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = await sut.Create(testItem, cancelToken);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_NotExistingIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            int notExistingId = 9999;
            Employee testItem = new Employee()
            {
                Id = notExistingId,
                FirstName = "tester",
                LastName = "ginger",
                Email = "gingin@gmail.com"
            };

            // Act
            var badResponse = await sut.Update(notExistingId, testItem, cancelToken);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Update_ExistingIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            int existingId = 2;
            Employee testItem = new Employee()
            {
                Id = existingId,
                FirstName = "tester",
                LastName = "ginger",
                Email = "gingin@gmail.com"
            };

            // Act
            var updatedRes = await sut.Update(existingId, testItem, cancelToken);

            // Assert
            Assert.IsType<OkObjectResult>(updatedRes);
        }

        #endregion

        #region Delete
        [Fact]
        public async void Remove_NotExistingIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            int notExistingId = 9999;

            // Act
            var badResponse = await sut.Delete(notExistingId, cancelToken);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Remove_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var cancelToken = CancellationToken.None;
            var sut = new EmployeesController(_service);
            var existingId = 1;

            // Act
            var res =  (OkObjectResult)await sut.Delete(existingId, cancelToken);

            // Assert
            res.StatusCode.Should().Be(200);
        }
      
        #endregion
    }
}
