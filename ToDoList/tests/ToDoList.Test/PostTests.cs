using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
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
            var controller = new ToDoItemsController();
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
            var controller = new ToDoItemsController();
            var itemToCreate = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            controller.Create(itemToCreate);
            var items = controller.Items;

            // Assert
            Assert.NotNull(controller.Items);
            Assert.Equal(1, items[0].ToDoItemId);
            Assert.Equal("Pondeli", items[0].Name);
            Assert.Equal("Vstavat!", items[0].Description);
            Assert.True(items[0].IsCompleted);
        }

        [Fact]
        public void Post_Item_CreateCorrectToDoItemId()
        {
            // Arrange
            var controller = new ToDoItemsController();
            var itemToCreateId1 = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);
            var itemToCreateId2 = new ToDoItemCreateRequestDto("Utery", "Pracovat!", true);
            var itemToCreateId3 = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            controller.Create(itemToCreateId1);
            controller.Create(itemToCreateId2);
            controller.Create(itemToCreateId3);
            var items = controller.Items;

            // Assert
            Assert.IsType<List<ToDoItem>>(controller.Items);
            Assert.Equal(3, items[2].ToDoItemId);
        }
    }
}
