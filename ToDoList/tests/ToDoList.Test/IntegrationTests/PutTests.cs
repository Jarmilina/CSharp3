using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;

using Xunit;

namespace ToDoList.Test.IntegrationTests
{
    public class PutTests
    {
        [Fact]
        public void Put_Item_ReturnsItem()
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
            var updateRequest = new ToDoItemUpdateRequestDto("Pondeli", "Day off!", false);

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemUpdateResponseDto;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ToDoItemUpdateResponseDto>(okResultValue);
        }

        [Fact]
        public void Put_Item_ItemIsUpdated()
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
            var updateRequest = new ToDoItemUpdateRequestDto("Pondeli", "Day off!", false);

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemUpdateResponseDto;

            // Assert
            Assert.Equal("Day off!", okResultValue.Description);
        }
    }
}
