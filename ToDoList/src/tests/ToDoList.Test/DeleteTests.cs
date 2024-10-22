using System;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Test;

public class DeleteTests
{
    private ToDoItemsController controller;
    private ToDoItem[] toDoItems;

    public DeleteTests()
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
    public void DeleteById_success()
    {
        //Arrange
        int id = 1;

        //Act
        var result = controller.DeleteById(1);

        //Assert
        result.Should().BeAssignableTo<NoContentResult>();
        Assert.Equal(ToDoItemsController.items.Count, 2);
    }

    [Fact]
    public void DeleteById_NotFound()
    {
        //Arrange
        int id = 5;

        //Act
        var result = controller.DeleteById(id);

        //Assert
        result.Should().BeAssignableTo<NotFoundResult>();
        Assert.Equal(ToDoItemsController.items.Count, 3);
    }
}
