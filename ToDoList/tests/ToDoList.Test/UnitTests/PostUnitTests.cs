using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
using Xunit;
using NSubstitute;

namespace ToDoList.Test.UnitTests
{
    public class PostUnitTests
    {
        [Fact]
        public void Post_Item_ReturnsCreated()
        {
            // Arrange
            // var options = new DbContextOptionsBuilder<ToDoItemsContext>()
            //     .UseSqlite("Data Source=:memory:;Mode=Memory;Cache=Shared")
            //     .Options;

            // using var context = new ToDoItemsContext(options);
            // context.Database.OpenConnection(); // Needed for in-memory databases
            // context.Database.EnsureCreated();
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var itemToCreate = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);

            // Act
            var result = controller.Create(itemToCreate);
            var createdResult = result.Result as CreatedResult;

            // Assert
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public void Post_UnhandeledExceptionRequest_Returns500()
        {
            //Arrange
            var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
            var controller = new ToDoItemsController(repositoryMock);
            var itemToCreate = new ToDoItemCreateRequestDto("Pondeli", "Vstavat!", true);
            repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

            // Act
            var result = controller.Create(itemToCreate);
            var createdResult = result.Result;

            // Assert
            Assert.IsType<ObjectResult>(createdResult);
            Assert.Equivalent(new StatusCodeResult(500), createdResult as StatusCodeResult);
        }
    }
}
