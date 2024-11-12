using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test.UnitTests
{
    public class ReadByIdTests
    {
        [Fact]
        public void Get_ReadAllAndSomeItemIsAvailable_ReturnOk()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.ReadAll().Returns(
                [
                    new ToDoItem{
                        Name = "testName",
                        Description = "testDescription",
                        IsCompleted = false
                    }
                ]);

            // Act
            var result = controller.Read();
            var resultResult = result.Result;
            var value = result.GetValue();

            // Assert
            Assert.IsType<OkObjectResult>(resultResult);
            Assert.NotNull(value);
            repositoryMock.Received(1).ReadAll();
        }
    }
}
