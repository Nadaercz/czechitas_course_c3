namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;

public class PutTests
{
    [Fact]
    public async Task Get_ReadAllAndSomeItemIsAvailable_ReturnOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAllAsync().Returns(
            [
                new ToDoItem{
                    Name = "testName",
                    Description = "testDescription",
                    IsCompleted = false,
                    Category = "HouseTasks"
                }
            ]);

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        await repositoryMock.Received(1).ReadAllAsync();
    }
}
