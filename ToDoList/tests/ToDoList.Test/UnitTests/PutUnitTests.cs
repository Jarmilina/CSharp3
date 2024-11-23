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
        private IRepositoryAsync<ToDoItem> repositoryMock;
        private ToDoItemsController controller;
        private ToDoItemUpdateRequestDto updateRequest;
        private ToDoItem updatedItem;

        public PutUnitTests()
        {
            repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            controller = new ToDoItemsController(repositoryMock);
            updateRequest = new ToDoItemUpdateRequestDto("Pondeli", "Day off!", "Všelijaké", false);
            updatedItem = new ToDoItem
            {
                Name = "Pondeli",
                Description = "Day off!",
                Category = "Všelijaké",
                IsCompleted = true
            };
        }

        [Fact]
        public async Task Put_UpdateByIdWhenItemUpdated_ReturnsItem()
        {
            // Arrange
            repositoryMock.UpdateByIdAsync(Arg.Any<ToDoItem>()).Returns(updatedItem);

            // Act
            var result = await controller.UpdateByIdAsync(1, updateRequest);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult?.Value as ToDoItemUpdateResponseDto;

            // Assert
            Assert.NotNull(okResult);
            repositoryMock.Received(1).UpdateByIdAsync(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                i.Category == "Všelijaké" &&
                !i.IsCompleted));
            Assert.Equal("Day off!", okResultValue?.Description);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ToDoItemUpdateResponseDto>(okResultValue);
        }

        [Fact]
        public async Task Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.UpdateByIdAsync(Arg.Any<ToDoItem>()).ReturnsNull();

            // Act
            var result = await controller.UpdateByIdAsync(1, updateRequest);
            var notFoundResult = result.Result as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            repositoryMock.Received(1).UpdateByIdAsync(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                !i.IsCompleted));
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status404NotFound), notFoundResult);
        }

        [Fact]
        public async Task Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            repositoryMock.When(r => r.UpdateByIdAsync(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

            // Act
            var result = await controller.UpdateByIdAsync(1, updateRequest);
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).UpdateByIdAsync(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Day off!" &&
                i.Category == "Všelijaké" &&
                !i.IsCompleted));
            repositoryMock.Received(0).UpdateByIdAsync(updatedItem);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
