namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;

public class DeleteByIdTests
{
    private readonly IRepositoryAsync<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;
    private readonly ToDoItem toDoItem;

    public DeleteByIdTests()
    {
        repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
        //předpřipravený
        toDoItem = new ToDoItem
        {
            Name = "testItem",
            Description = "testDescription",
            IsCompleted = false,
            ToDoItemId = 1
        };
    }

    [Fact]
    public async Task Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        //možnost A - ReadById pro jakýkoliv argument vrátí náš předpřipravený toDoItem
        await repositoryMock.ReadByIdAsync(Arg.Any<int>());

        // Act
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        //mock zaregistroval jedno volání metody Delete s argumentem toDoItem
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
        //mock zaregistroval jedno volání metody ReadById s argumentem toDoItem.ToDoItemId
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
        await repositoryMock.Received(1).DeleteByIdAsync(Arg.Any<int>());
    }
}
