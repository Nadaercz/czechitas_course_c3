namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute.ExceptionExtensions;

public class GetUnitTests
{
    [Fact]
    public void Get_ReadAllAndSomeItemIsAvailable_ReturnOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        //repositoryMock.When().Do();
        // repositoryMock.ReadAll().Throws();
        // repositoryMock.Received().ReadAll();
        repositoryMock.Read().Returns(
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
        repositoryMock.Received(1).Read();
    }

    [Fact]
    public void Get_ReadAllExceptionOccured_ReturnInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.Read().Throws(new Exception());

        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).Read();
    }
}
