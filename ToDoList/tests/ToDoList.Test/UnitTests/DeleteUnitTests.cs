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
    public class DeleteUnitTests
    {
        private IRepositoryAsync<ToDoItem> repositoryMock;
        private ToDoItemsController controller;
        private List<ToDoItem> items;

        public DeleteUnitTests()
        {
            repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            controller = new ToDoItemsController(repositoryMock);
            items = new List<ToDoItem>
            {
                new ToDoItem
                {
                    ToDoItemId = 1,
                    Name = "Pondeli",
                    Description = "Vstavat!",
                    IsCompleted = true
                },
                new ToDoItem
                {
                    ToDoItemId = 2,
                    Name = "Utery",
                    Description = "Pracovat!",
                    IsCompleted = true
                }
            };
        }

        [Fact]
        public async Task Delete_DeleteByIdValidItemId_ReturnsNoContent()
        {
            // Arrange
            repositoryMock.DeleteByIdAsync(Arg.Any<int>()).Returns(items[0]);

            // Act
            var result = await controller.DeleteByIdAsync(1);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkResult>(okResult);
            repositoryMock.Received(1).DeleteByIdAsync(1);
        }

        [Fact]
        public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.DeleteByIdAsync(Arg.Any<int>()).ReturnsNull();

            // Act
            var notFoundResult = await controller.DeleteByIdAsync(4);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            repositoryMock.Received(1).DeleteByIdAsync(4);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status404NotFound), notFoundResult);
        }
    }
}
