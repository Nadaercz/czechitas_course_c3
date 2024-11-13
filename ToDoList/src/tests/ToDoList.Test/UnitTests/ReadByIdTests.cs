using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

//evrywhere else we are using file-scoped namespace. This is ok, but I would be better if we use it here also.
//it looks better if we have one convention and use it
namespace ToDoList.Test.UnitTests
{
    public class ReadByIdTests
    {
        /*
        We should be testing ReadById method here, not Read...


        Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
        Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
        Get_ReadByIdUnhandledException_ReturnsInternalServerError()
        */
        
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
