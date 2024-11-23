using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test.UnitTests
{
    public class GetUnitTests
    {
        private IRepositoryAsync<ToDoItem> repositoryMock;
        private ToDoItemsController controller;
        private List<ToDoItem> items;

        public GetUnitTests()
        {
            repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            controller = new ToDoItemsController(repositoryMock);
            items = new List<ToDoItem>
            {
                new ToDoItem
                {
                    ToDoItemId = 1,
                    Name = "Pondělí",
                    Description = "Vstávat!",
                    Category = null,
                    IsCompleted = true
                },
                new ToDoItem
                {
                    ToDoItemId = 2,
                    Name = "Úterý",
                    Description = "Pracovat!",
                    Category = "Práce",
                    IsCompleted = true
                },
                new ToDoItem
                {
                    ToDoItemId = 3,
                    Name = "Středa",
                    Description = "Odpočívat!",
                    Category = "Well=being",
                    IsCompleted = true
                }
            };
        }

        [Fact]
        public async Task Get_ReadWhenSomeItemAvailable_ReturnsOkResultWithItems()
        {
            // Arrange
            repositoryMock.ReadAsync().Returns(Task.FromResult<IEnumerable<ToDoItem>>(items));

            // Act
            var result = await controller.ReadAsync();
            var okResult = result.Result as OkObjectResult;
            var okValue = okResult.Value;

            // Assert
            repositoryMock.Received(1).ReadAsync();
            Assert.NotNull(okValue);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<List<ToDoItemReadResponseDto>>(okValue);
        }

        [Fact]
        public async Task Get_AllItems_ReturnsCorrectItems()
        {
            // Arrange
            repositoryMock.ReadAsync().Returns(Task.FromResult<IEnumerable<ToDoItem>>(items));

            // Act
            var result = await controller.ReadAsync();
            var okResult = result.Result as OkObjectResult;
            var itemsResult = okResult?.Value as List<ToDoItemReadResponseDto>;

            // Assert
            repositoryMock.Received(1).ReadAsync();
            Assert.Equal("Pondělí", itemsResult[0].Name);
            Assert.Equal("Vstávat!", itemsResult[0].Description);
            Assert.Null(itemsResult[0].Category);
            Assert.True(itemsResult[0].IsCompleted);
        }

        [Fact]
        public async Task Get_AllItems_ReturnsAllItems()
        {
            // Arrange
            repositoryMock.ReadAsync().Returns(items);

            // Act
            var result = await controller.ReadAsync();
            var okResult = result.Result as OkObjectResult;
            var itemsToDisplay = okResult.Value as List<ToDoItemReadResponseDto>;

            // Assert
            repositoryMock.Received(1).ReadAsync();
            Assert.IsType<List<ToDoItemReadResponseDto>>(itemsToDisplay);
            Assert.Equal(3, itemsToDisplay.Count);
        }

        [Fact]
        public async Task Get_ReadWhenNoItemAvailable_ReturnsEmptyListWhenNoItems()
        {
            // Arrange
            List<ToDoItem> emptyItems = [];
            repositoryMock.ReadAsync().Returns(emptyItems);

            // Act
            var result = await controller.ReadAsync();
            var okResult = result.Result as OkObjectResult;
            var itemsResult = okResult.Value as List<ToDoItemReadResponseDto>;
            //Tady bych si chtela nechat poslat ok s prazdnym seznamem
            //a az na FE se podle prazdneho obsahu seznamu rozhodnout,
            //co zobrazim.

            // Assert
            repositoryMock.Received(1).ReadAsync();
            Assert.Empty(itemsResult);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            repositoryMock.ReadAsync().Throws(new Exception());

            // Act
            var result = await controller.ReadAsync();
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).ReadAsync();
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }

        [Fact]
        public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsItem()
        {
            // Arrange
            repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = await controller.ReadByIdAsync(2);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemReadResponseDto;

            // Assert
            repositoryMock.Received(1).ReadByIdAsync(2);
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ToDoItemReadResponseDto>(okResultValue);
        }

        [Fact]
        public async Task Get_ItemById_ReturnsCorrectItem()
        {
            // Arrange
            repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = await controller.ReadByIdAsync(2);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemReadResponseDto;

            // Assert
            repositoryMock.Received(1).ReadByIdAsync(2);
            Assert.Equal("Úterý", okResultValue.Name);
            Assert.Equal("Pracovat!", okResultValue.Description);
            Assert.Equal("Práce", okResultValue.Category);
            Assert.True(okResultValue.IsCompleted);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsItem()
        {
            // Arrange
            repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = await controller.ReadByIdAsync(2);
            var resultResult = result.Result;
            var value = result.GetValue();

            // Assert
            Assert.IsType<OkObjectResult>(resultResult);
            Assert.NotNull(value);

            Assert.Equal(items[1].Description, value.Description);
            Assert.Equal(items[1].IsCompleted, value.IsCompleted);
            Assert.Equal(items[1].Name, value.Name);
        }

        [Fact]
        public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
        {
            // Arrange
            repositoryMock.ReadByIdAsync(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await controller.ReadByIdAsync(1);
            var noResult = result.Result as NotFoundResult;

            // Assert
            repositoryMock.Received(1).ReadByIdAsync(1);
            Assert.IsType<NotFoundResult>(noResult);
        }

        [Fact]
        public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            repositoryMock.ReadByIdAsync(Arg.Any<int>()).Throws(new Exception());

            // Act
            var result = await controller.ReadByIdAsync(1);
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).ReadByIdAsync(1);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
