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
        [Fact]
        public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var items = new List<ToDoItem>
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
                },
                new ToDoItem
                {
                    ToDoItemId = 3,
                    Name = "Streda",
                    Description = "Odpocivat!",
                    IsCompleted = true
                }
            };
            repositoryMock.DeleteById(Arg.Any<int>()).Returns(items[0]);

            // Act
            var result = controller.DeleteById(1);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkResult>(okResult);
            repositoryMock.Received(1).DeleteById(1);
        }

        [Fact]
        public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.DeleteById(Arg.Any<int>()).ReturnsNull();

            // Act
            var notFoundResult = controller.DeleteById(4);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            repositoryMock.Received(1).DeleteById(4);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status404NotFound), notFoundResult);
        }
    }
}
