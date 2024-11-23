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
        private IRepositoryAsync<ToDoItem> repositoryMock;
        private ToDoItemsController controller;
        private ToDoItemCreateRequestDto itemRequest;

        public PostUnitTests()
        {
            repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
            controller = new ToDoItemsController(repositoryMock);
            itemRequest = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", "Všelijaké", true);
        }

        [Fact]
        public async Task Post_CreateValidRequest_ReturnsCreatedAtAction()
        {
            // Act
            var result = await controller.CreateAsync(itemRequest);
            var createdResult = result.Result as CreatedResult;

            // Assert
            Assert.IsType<CreatedResult>(createdResult);
            repositoryMock.Received(1).CreateAsync(Arg.Is<ToDoItem>(i =>
                i.Name == "Pondeli" &&
                i.Description == "Vstavat!" &&
                i.Category == "Všelijaké" &&
                i.IsCompleted));
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public async Task Post_CreateUnhandledException_ReturnsInternalServerError()
        {
            //Arrange
            repositoryMock.When(r => r.CreateAsync(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

            // Act
            var result = await controller.CreateAsync(itemRequest);
            var item = itemRequest.ToDomain();
            var errorResult = result.Result as ObjectResult;

            // Assert
            Assert.IsType<ObjectResult>(errorResult);
            repositoryMock.Received(0).CreateAsync(item);
            Assert.Equal(500, errorResult?.StatusCode);
            Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), errorResult);
        }
    }
}
