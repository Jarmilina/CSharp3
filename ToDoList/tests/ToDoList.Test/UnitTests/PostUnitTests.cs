using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using NSubstitute;
using Microsoft.AspNetCore.Http;

namespace ToDoList.Test.UnitTests
{
    public class PostUnitTests
    {
        [Fact]
        public void Post_CreateValidRequest_ReturnsCreatedAtAction()
        {
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var itemRequest = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", "Všelijaké", true);

            // Act
            var result = controller.Create(itemRequest);
            var createdResult = result.Result as CreatedResult;

            // Assert
            Assert.IsType<CreatedResult>(createdResult);
            repositoryMock.Received(1).Create(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Vstavat!" &&
                i.IsCompleted));
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public void Post_CreateUnhandledException_ReturnsInternalServerError()
        {
            //Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var itemRequest = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", "Všelijaké", true);
            repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

            // Act
            var result = controller.Create(itemRequest);
            var item = itemRequest.ToDomain();
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(0).Create(item);
            // Assert.Equal(500, createdResult?.StatusCode);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
