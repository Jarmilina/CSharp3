using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test.UnitTests
{
    public class PutUnitTests
    {
        [Fact]
        public void Put_UpdateByIdWhenItemUpdated_ReturnsItem()
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
            repositoryMock.UpdateById(Arg.Any<ToDoItem>()).Returns(updatedItem);

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult?.Value as ToDoItemUpdateResponseDto;

            // Assert
            Assert.NotNull(okResult);
            repositoryMock.Received(1).UpdateById(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                !i.IsCompleted));
            Assert.Equal("Day off!", okResultValue?.Description);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ToDoItemUpdateResponseDto>(okResultValue);
        }

        [Fact]
        public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
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
            repositoryMock.UpdateById(Arg.Any<ToDoItem>()).ReturnsNull();

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            repositoryMock.Received(1).UpdateById(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                !i.IsCompleted));
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status404NotFound), notFoundResult);
        }

        [Fact]
        public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
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
            repositoryMock.When(r => r.UpdateById(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

            // Act
            var result = controller.UpdateById(1, updateRequest);
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).UpdateById(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                !i.IsCompleted));
            repositoryMock.Received(0).UpdateById(updatedItem);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
