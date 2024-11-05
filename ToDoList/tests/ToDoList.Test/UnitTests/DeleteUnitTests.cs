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
    public class DeleteUnitTests
    {
        [Fact]
        public void Delete_Item_ReturnsOk()
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
            repositoryMock.DeleteById(1).Returns(items[0]);


            // Act
            var result = controller.DeleteById(1);
            var okResult = result as OkResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public void Delete_NonExistantItemId_ThrowsException()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.DeleteById(4).Returns((ToDoItem?)null);

            // Act
            var result = controller.DeleteById(4);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
