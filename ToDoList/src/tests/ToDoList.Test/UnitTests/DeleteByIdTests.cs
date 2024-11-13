using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test.UnitTests
{
    public class DeleteByIdTests
    {
        private readonly IRepository<ToDoItem> repositoryMock;
        private readonly ToDoItemsController controller;
        private readonly ToDoItem toDoItem;

        public DeleteByIdTests()
        {
            repositoryMock = Substitute.For<IRepository<ToDoItem>>();
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
        public void Delete_ValidItemId_ReturnsNoContent()
        {
            // Arrange
            //možnost A - ReadById pro jakýkoliv argument vrátí náš předpřipravený toDoItem
            repositoryMock.ReadById(Arg.Any<int>());

            // Act
            var result = controller.DeleteById(toDoItem.ToDoItemId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            //mock zaregistroval jedno volání metody Delete s argumentem toDoItem
            repositoryMock.Received(1).DeleteById(toDoItem.ToDoItemId);
        }

        [Fact]
        public void Delete_DeleteById_ReturnsNotFound()
        {
            //Arrange
            int itemId = 4;
            repositoryMock.When(x => x.DeleteById(itemId)).Throw<ArgumentOutOfRangeException>();

            // Act
            var result = controller.DeleteById(itemId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            //mock zaregistroval jedno volání metody ReadById s argumentem toDoItem.ToDoItemId
            repositoryMock.Received(1).DeleteById(itemId);
        }

        [Fact]
        public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            // ... the rest of the setup
            repositoryMock.When(x => x.DeleteById(toDoItem.ToDoItemId)).Throw<Exception>();

            // Act
            var result = controller.DeleteById(toDoItem.ToDoItemId);

            // Assert
            Assert.IsType<ObjectResult>(result);
            repositoryMock.Received(1).DeleteById(Arg.Any<int>());
        }
    }
}
