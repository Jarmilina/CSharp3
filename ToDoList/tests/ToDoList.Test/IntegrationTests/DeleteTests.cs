using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test.IntegrationTests
{
    public class DeleteTests
    {
        [Fact]
        public void Delete_Item_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ToDoItemsContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            using var context = new ToDoItemsContext(options);
            context.Database.OpenConnection(); // Needed for in-memory databases
            context.Database.EnsureCreated();
            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Vstavat!",
                IsCompleted = true
            };
            context.ToDoItems.Add(toDoItem);
            context.SaveChanges();
            var controller = new ToDoItemsController(context);

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
            var options = new DbContextOptionsBuilder<ToDoItemsContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            using var context = new ToDoItemsContext(options);
            context.Database.OpenConnection(); // Needed for in-memory databases
            context.Database.EnsureCreated();

            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Vstavat!",
                IsCompleted = true
            };
            context.ToDoItems.Add(toDoItem);
            var controller = new ToDoItemsController(context);

            // Act
            controller.DeleteById(1);
            var deletedItem = context.ToDoItems.ToList().Find(o => o.ToDoItemId == 1);

            // Assert
            Assert.Null(deletedItem);
        }
    }
}
