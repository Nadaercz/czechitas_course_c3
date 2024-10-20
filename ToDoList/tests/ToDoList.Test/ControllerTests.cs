using ToDoList.Domain;
using ToDoList.WebApi;

namespace ToDoList.Test;

using ToDoList.WebApi.Controllers;

public class ControllerTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem();
        ToDoItemsController.items.Add(toDoItem);
        // Act
        var result = controller.Read();
        var value = result.Value;
        var resultResult = result.Result;
        // Assert
        Assert.True(resultResult is OkObjectResult);
        Assert.IsType<OkObjectResult>(resultResult);
    }

    /* spravny pocet objectu
        stejne objecty
        
    */
}

