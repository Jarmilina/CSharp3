using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;


namespace ToDoList.Test
{
    public class GetTests
    {
        [Fact]
        public void Get_AllItems_ReturnsAllItems()
        {
            // Arrange
            var controller = new ToDoItemsController();
            var toDoItem = new ToDoItem();
            ToDoItemsController.Items.Add(toDoItem);

            // Act
            var result = controller.Read();
            var value = result.Value;
            var resultResult = result.Result;

            // Assert
            Assert.True(resultResult is OkObjectResult);
            Assert.IsType<OkObjectResult>(resultResult);

        }
    }
}
