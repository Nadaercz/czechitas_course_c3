namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using NSubstitute.ExceptionExtensions;
using Microsoft.AspNetCore.Http;

public class GetUnitTests //again, we have different name for cs file and class
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
        //i would also check that we got back IEnumerable<ToDoItem> with count = mock count - to check that our list did not magically be larger that it should be
    }

    [Fact]
    public void Get_ReadAllExceptionOccured_ReturnInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAll().Throws<Exception>();

        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        var value = result.GetValue(); //we do not need it here

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
        //check status code :) we want to check that we got 500
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
