using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
using Xunit;

namespace ToDoList.Test
{
    public class PutTests
    {
        [Fact]
        public void Put_Item_ReturnsItem()
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
            var controller = new ToDoItemsController();
            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Vstavat!",
                IsCompleted = true
            };
            controller.Items.Add(toDoItem);
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
