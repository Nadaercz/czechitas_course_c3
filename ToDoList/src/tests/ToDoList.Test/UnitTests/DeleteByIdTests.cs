namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

public class DeleteByIdTests
{
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;
    private readonly ToDoItem toDoItem;

    public DeleteByIdTests()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
        toDoItem = new ToDoItem
        {
            Name = "testItem",
            Description = "testDescription",
            IsCompleted = false,
            ToDoItemId = 1,
            Category = "HouseTasks"
        };
    }

    [Fact]
    public async Task Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        await repositoryMock.ReadByIdAsync(Arg.Any<int>());

        // Act
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).DeleteByIdAsync(toDoItem.ToDoItemId);
    }

    [Fact]
    public async Task Delete_DeleteById_ReturnsNotFound()
    {
        //Arrange
        int itemId = 4;
        repositoryMock.When(x => x.DeleteByIdAsync(itemId)).Throw<ArgumentOutOfRangeException>();

        // Act
        var result = await controller.DeleteByIdAsync(itemId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
        await repositoryMock.Received(1).DeleteByIdAsync(itemId);
    }

    [Fact]
    public async Task Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        // ... the rest of the setup
        repositoryMock.When(x => x.DeleteByIdAsync(toDoItem.ToDoItemId)).Throw<Exception>();

        // Act
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        await repositoryMock.Received(1).DeleteByIdAsync(toDoItem.ToDoItemId);
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
    }
}
