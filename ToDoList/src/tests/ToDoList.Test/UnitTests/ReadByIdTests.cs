namespace ToDoList.Test.UnitTests;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class ReadByIdTests
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
