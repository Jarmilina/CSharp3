// namespace ToDoList.Test;

// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ToDoList.Domain.Models;
// using ToDoList.Persistence;
// using ToDoList.WebApi.Controllers;

// public class GetByIdTests
// {
//     [Fact]
//     public void GetById_ValidId_ReturnsItem()
//     {
//         // Arrange
//         var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//         using var context = new ToDoItemsContext(options);
//         context.Database.OpenConnection(); // Needed for in-memory databases
//         context.Database.EnsureCreated();
//         var toDoItem = new ToDoItem
//         {
//             ToDoItemId = 1,
//             Name = "Jmeno",
//             Description = "Popis",
//             IsCompleted = false
//         };
//         context.ToDoItems.Add(toDoItem);
//         context.SaveChanges();
//         var controller = new ToDoItemsController(context);

//         // Act
//         var result = controller.ReadById(toDoItem.ToDoItemId);
//         var resultResult = result.Result;
//         var value = result.GetValue();

//         // Assert
//         Assert.IsType<OkObjectResult>(resultResult);
//         Assert.NotNull(value);

//         Assert.Equal(toDoItem.Description, value.Description);
//         Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
//         Assert.Equal(toDoItem.Name, value.Name);
//     }

//     [Fact]
//     public void GetById_InvalidId_ReturnsNotFound()
//     {
//         // Arrange
//         var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//         using var context = new ToDoItemsContext(options);
//         context.Database.OpenConnection(); // Needed for in-memory databases
//         context.Database.EnsureCreated();

//         var toDoItem = new ToDoItem
//         {
//             ToDoItemId = 1,
//             Name = "Jmeno",
//             Description = "Popis",
//             IsCompleted = false
//         };
//         context.ToDoItems.Add(toDoItem);
//         context.SaveChanges();
//         var controller = new ToDoItemsController(context);

//         // Act
//         var invalidId = -1;
//         var result = controller.ReadById(invalidId);
//         var resultResult = result.Result;

//         // Assert
//         Assert.IsType<NotFoundResult>(resultResult);
//     }
// }
