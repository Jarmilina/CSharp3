using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class DeleteTests
    {
        [Fact]
        public void Delete_Item_ReturnsOk()
        {
            // Arrange
            var controller = new ToDoItemsController();
            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Vstavat!",
                IsCompleted = true
            };
            controller.Items.Add(toDoItem);

            // Act
            var result = controller.DeleteById(1);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public void Delete_Item_DeletesItem()
        {
            // Arrange
            var controller = new ToDoItemsController();
            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Vstavat!",
                IsCompleted = true
            };
            controller.Items.Add(toDoItem);

            // Act
            controller.DeleteById(1);
            var deletedItem = controller.Items.Find(o => o.ToDoItemId == 1);

            // Assert
            Assert.Null(deletedItem);
        }
    }
}
