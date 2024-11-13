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
        public void Get_ReadWhenSomeItemAvailable_ReturnsOkResultWithItems()
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
            repositoryMock.Read().Returns(items);

            // Act
            var result = controller.Read();
            var okResult = result.Result as OkObjectResult;
            var okValue = okResult.Value;

            // Assert
            repositoryMock.Received(1).Read();
            Assert.NotNull(okValue);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<List<ToDoItemReadResponseDto>>(okValue);
        }

        [Fact]
        public void Get_AllItems_ReturnsCorrectItems()
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
            repositoryMock.Read().Returns(items);

            // Act
            var result = controller.Read();
            var okResult = result.Result as OkObjectResult;
            var itemsResult = okResult?.Value as List<ToDoItemReadResponseDto>;

            // Assert
            repositoryMock.Received(1).Read();
            Assert.Equal("Pondeli", itemsResult[0].Name);
            Assert.Equal("Vstavat!", itemsResult[0].Description);
            Assert.True(itemsResult[0].IsCompleted);
        }

        [Fact]
        public void Get_AllItems_ReturnsAllItems()
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
            repositoryMock.Read().Returns(items);

            // Act
            var result = controller.Read();
            var okResult = result.Result as OkObjectResult;
            var itemsToDisplay = okResult.Value as List<ToDoItemReadResponseDto>;

            // Assert
            repositoryMock.Received(1).Read();
            Assert.IsType<List<ToDoItemReadResponseDto>>(itemsToDisplay);
            Assert.Equal(3, itemsToDisplay.Count);
        }

        [Fact]
        public void Get_ReadWhenNoItemAvailable_ReturnsEmptyListWhenNoItems()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);

            IEnumerable<ToDoItem> items = new List<ToDoItem>();
            repositoryMock.Read().Returns(items);

            // Act
            var result = controller.Read();
            var okResult = result.Result as OkObjectResult;
            var itemsResult = okResult.Value as List<ToDoItemReadResponseDto>;
            //Tady bych si chtela nechat poslat ok s prazdnym seznamem
            //a az na FE se podle prazdneho obsahu seznamu rozhodnout,
            //co zobrazim.

            // Assert
            repositoryMock.Received(1).Read();
            Assert.Empty(itemsResult);
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_ReadUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.Read().Throws(new Exception());

            // Act
            var result = controller.Read();
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).Read();
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }

        [Fact]
        public void Get_ReadByIdWhenSomeItemAvailable_ReturnsItem()
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
            repositoryMock.ReadById(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = controller.ReadById(2);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemReadResponseDto;

            // Assert
            repositoryMock.Received(1).ReadById(2);
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            Assert.IsType<ToDoItemReadResponseDto>(okResultValue);
        }

        [Fact]
        public void Get_ItemById_ReturnsCorrectItem()
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
            repositoryMock.ReadById(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = controller.ReadById(2);
            var okResult = result.Result as OkObjectResult;
            var okResultValue = okResult.Value as ToDoItemReadResponseDto;

            // Assert
            repositoryMock.Received(1).ReadById(2);
            Assert.Equal("Utery", okResultValue.Name);
            Assert.Equal("Pracovat!", okResultValue.Description);
            Assert.True(okResultValue.IsCompleted);
        }

        [Fact]
        public void GetById_ValidId_ReturnsItem()
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
            repositoryMock.ReadById(Arg.Any<int>()).Returns(items[1]);

            // Act
            var result = controller.ReadById(2);
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
        public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);

            IEnumerable<ToDoItem> items = new List<ToDoItem>();
            repositoryMock.ReadById(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = controller.ReadById(1);
            var noResult = result.Result as NotFoundResult;

            // Assert
            repositoryMock.Received(1).ReadById(1);
            Assert.IsType<NotFoundResult>(noResult);
        }

        [Fact]
        public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
        {
            // Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

            // Act
            var result = controller.ReadById(1);
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(1).ReadById(1);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
