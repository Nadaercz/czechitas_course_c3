namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using FluentAssertions;

public class GetTests
{
    private ToDoItemsController controller;
    private ToDoItem[] toDoItems;

    public GetTests()
    {
        controller = new ToDoItemsController();
        toDoItems =
        [
            new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Jmeno1",
                Description = "Popis1",
                IsCompleted = false
            },
            new ToDoItem
            {
                ToDoItemId = 2,
                Name = "Jmeno2",
                Description = "Popis2",
                IsCompleted = true
            },
            new ToDoItem
            {
                ToDoItemId = 3,
                Name = "Jmeno3",
                Description = "Popis3",
                IsCompleted = false
            }
        ];
        ToDoItemsController.items.AddRange(toDoItems);
    }

    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Act
        var result = controller.Read();

        // Assert
        var resultResult = result.Result;
        var value = result.GetValue();
        resultResult.Should().BeAssignableTo<OkObjectResult>();
        Assert.NotNull(value);
        Assert.Equal(toDoItems.Length, value.Count());
        value.Should().BeEquivalentTo(toDoItems, item => item.Excluding(x => x.ToDoItemId));
    }

    [Fact]
    public void Get_AllItems_NotFound()
    {
        // Arrange
        ToDoItemsController.items.Clear();

        // Act
        var result = controller.Read();

        // Assert
        var resultResult = result.Result;
        resultResult.Should().BeAssignableTo<NotFoundResult>();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Get_ItemById_ReturnsItem(int id)
    {
        // Act
        var result = controller.ReadById(id);

        // Assert
        var resultResult = result.Result;
        var value = result.GetValue();
        resultResult.Should().BeAssignableTo<OkObjectResult>();
        Assert.NotNull(value);
        value.Should().BeEquivalentTo(toDoItems[id-1], item => item.Excluding(x => x.ToDoItemId));
    }

    [Fact]
    public void Get_Item_NotFound()
    {
        // Arrange
        int id = 5;

        // Act
        var result = controller.ReadById(id);

        // Assert
        var resultResult = result.Result;
        resultResult.Should().BeAssignableTo<NotFoundResult>();
    }
}