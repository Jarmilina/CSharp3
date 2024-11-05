using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test.UnitTests
{
    public class PutUnitTests
    {
        [Fact]
        public void Put_Item_ReturnsItem()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var updateRequest = new ToDoItemUpdateRequestDto("Pondeli", "Day off!", false);
            var updatedItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Day off!",
                IsCompleted = true
            };
            repositoryMock.UpdateById(1, updateRequest).Returns(updatedItem);

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
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var updateRequest = new ToDoItemUpdateRequestDto("Pondeli", "Day off!", false);
            var updatedItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "Day off!",
                IsCompleted = true
            };
            repositoryMock.UpdateById(1, updateRequest).Returns(updatedItem);

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemUpdateResponseDto;

            // Assert
            Assert.Equal("Day off!", okResultValue.Description);
        }
    }
}
