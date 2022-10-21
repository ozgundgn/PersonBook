using Castle.Core.Logging;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PhoneBook.UI.Controllers;
using PhoneBookUI.Test.Models;
using Xunit;

namespace PhoneBookUI.Test
{
    public class HomeControllerTest
    {
        private ILogger<HomeController> _logger;
        public HomeControllerTest()
        {
            var mock = new Mock<ILogger<HomeController>>();
            _logger = mock.Object;
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_PersonIndex()
        {
            // Arrange

            var controller = new HomeController(_logger);
            // Act
            var result = controller.Index();
            // Assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async Task PersonList_ReturnsAViewResult_GetPersons()
        {
            // Arrange
            var controller = new HomeController(_logger);
            // Act
            var result = controller.PersonList(new GetAllPersonsQuery());
            // Assert
            Assert.IsType<OkResult>(result);
        }


        [Theory]
        [ClassData(typeof(CreatePersonCommandTestData))]
        public async Task PersonSave_ReturnsAViewResult_ReturnOk(CreatePersonCommand person)
        {
            // Arrange
            var controller = new HomeController(_logger);
            // Act
            var result = controller.PersonSave(person);
            // Assert
            Assert.IsType<OkResult>(result);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public async Task PersonDelete_ReturnsAViewResult_ReturnOk(int person)
        {
            // Arrange
            var controller = new HomeController(_logger);
            // Act
            var result = controller.PersonDelete(person);
            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(-1)]

        public async Task PersonDelete_ReturnsAViewResult_ReturnBadRequest(int person)
        {
            // Arrange
            var controller = new HomeController(_logger);
            // Act
            var result = controller.PersonDelete(person);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

    }
}