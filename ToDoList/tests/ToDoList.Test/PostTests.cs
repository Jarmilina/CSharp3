using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class PostTests
    {
        [Fact]
        public void Post_Item_ReturnsCreated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ToDoItemsContext>()
                .UseSqlite("Data Source=:memory:;Mode=Memory;Cache=Shared")
                .Options;

            using var context = new ToDoItemsContext(options);
            context.Database.OpenConnection(); // Needed for in-memory databases
            context.Database.EnsureCreated();
            var controller = new ToDoItemsController(context);
            var itemToCreate = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            var result = controller.Create(itemToCreate);
            var createdResult = result.Result as CreatedResult;

            // Assert
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public void Post_Item_SavesItemCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ToDoItemsContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            using var context = new ToDoItemsContext(options);
            context.Database.OpenConnection(); // Needed for in-memory databases
            context.Database.EnsureCreated();
            var controller = new ToDoItemsController(context);
            var itemToCreate = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            controller.Create(itemToCreate);
            var items = context.ToDoItems.ToList();

            // Assert
            Assert.NotNull(items);
            Assert.Equal(1, items[0].ToDoItemId);
            Assert.Equal("Pondeli", items[0].Name);
            Assert.Equal("Vstavat!", items[0].Description);
            Assert.True(items[0].IsCompleted);
        }

        [Fact]
        public void Post_Item_CreateCorrectToDoItemId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ToDoItemsContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            using var context = new ToDoItemsContext(options);
            context.Database.OpenConnection(); // Needed for in-memory databases
            context.Database.EnsureCreated();
            var controller = new ToDoItemsController(context);
            var itemToCreateId1 = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);
            var itemToCreateId2 = new ToDoItemCreateRequestDto("Utery", "Pracovat!", true);
            var itemToCreateId3 = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            controller.Create(itemToCreateId1);
            controller.Create(itemToCreateId2);
            controller.Create(itemToCreateId3);
            var items = context.ToDoItems.ToList();

            // Assert
            Assert.IsType<List<ToDoItem>>(items);
            Assert.Equal(3, items[2].ToDoItemId);
        }
    }
}
