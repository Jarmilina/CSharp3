// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ToDoList.Domain.DTOs;
// using ToDoList.Domain.Models;
// using ToDoList.Persistence;
// using ToDoList.WebApi.Controllers;


// namespace ToDoList.Test.IntegrationTests
// {
//     public class GetTests
//     {
//         [Fact]
//         public void Get_AllItems_ReturnsOkResultWithItems()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();

//             var newItems = new List<ToDoItem>
//             {
//                 new ToDoItem
//                 {
//                     ToDoItemId = 1,
//                     Name = "Pondeli",
//                     Description = "Vstavat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 2,
//                     Name = "Utery",
//                     Description = "Pracovat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 3,
//                     Name = "Streda",
//                     Description = "Odpocivat!",
//                     IsCompleted = true
//                 }
//             };

//             context.ToDoItems.AddRange(newItems);
//             context.SaveChanges();

//             var controller = new ToDoItemsController(context);
//             // var toDoItem = new ToDoItem();
//             // var items = context.ToDoItems.ToList();
//             // items.Add(toDoItem);

//             // Act
//             var result = controller.Read();
//             var okResult = result.Result as OkObjectResult;
//             var okValue = okResult.Value;

//             // Assert
//             // Assert.True(resultResult is OkObjectResult);
//             Assert.NotNull(context.ToDoItems);
//             Assert.IsType<OkObjectResult>(okResult);
//             Assert.IsType<List<ToDoItemReadResponseDto>>(okValue);
//         }

//         [Fact]
//         public void Get_AllItems_ReturnsCorrectItems()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();

//             var toDoItem = new ToDoItem
//             {
//                 ToDoItemId = 1,
//                 Name = "Pondeli",
//                 Description = "Vstavat!",
//                 IsCompleted = true
//             };
//             context.ToDoItems.Add(toDoItem);
//             context.SaveChanges();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.Read();
//             var okResult = result.Result as OkObjectResult;
//             var items = okResult.Value as List<ToDoItemReadResponseDto>;

//             // Assert
//             Assert.Equal("Pondeli", items[0].Name);
//             Assert.Equal("Vstavat!", items[0].Description);
//             Assert.True(items[0].IsCompleted);
//         }

//         [Fact]
//         public void Get_AllItems_ReturnsAllItems()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();
//             var newItems = new List<ToDoItem>
//             {
//                 new ToDoItem
//                 {
//                     ToDoItemId = 1,
//                     Name = "Pondeli",
//                     Description = "Vstavat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 2,
//                     Name = "Utery",
//                     Description = "Pracovat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 3,
//                     Name = "Streda",
//                     Description = "Odpocivat!",
//                     IsCompleted = true
//                 }
//             };
//             context.ToDoItems.AddRange(newItems);
//             context.SaveChanges();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.Read();
//             var okResult = result.Result as OkObjectResult;
//             var itemsToDisplay = okResult.Value as List<ToDoItemReadResponseDto>;

//             // Assert
//             Assert.IsType<List<ToDoItemReadResponseDto>>(itemsToDisplay);
//             Assert.Equal(3, itemsToDisplay.Count);
//         }

//         [Fact]
//         public void Get_AllItems_ReturnsEmptyListWhenNoItems()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.Read();
//             var okResult = result.Result as OkObjectResult;
//             var items = okResult.Value as List<ToDoItemReadResponseDto>;
//             //Tady bych si chtela nechat poslat ok s prazdnym seznamem
//             //a az na FE se podle prazdneho obsahu seznamu rozhodnout,
//             //co zobrazim.

//             // Assert
//             Assert.Empty(items);
//             Assert.IsType<OkObjectResult>(okResult);
//         }

//         [Fact]
//         public void Get_ItemById_ReturnsItem()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();
//             var toDoItem = new ToDoItem
//             {
//                 ToDoItemId = 1,
//                 Name = "Pondeli",
//                 Description = "vstavat",
//                 IsCompleted = true
//             };
//             context.ToDoItems.Add(toDoItem);
//             context.SaveChanges();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.ReadById(1);
//             var okResult = result.Result as OkObjectResult;
//             var okResultValue = okResult.Value as ToDoItemReadResponseDto;

//             // Assert
//             Assert.NotNull(okResult);
//             Assert.IsType<OkObjectResult>(okResult);
//             Assert.IsType<ToDoItemReadResponseDto>(okResultValue);
//         }

//         [Fact]
//         public void Get_ItemById_ReturnsCorrectItem()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();
//             var newItems = new List<ToDoItem>
//             {
//                 new ToDoItem
//                 {
//                     ToDoItemId = 1,
//                     Name = "Pondeli",
//                     Description = "Vstavat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 2,
//                     Name = "Utery",
//                     Description = "Pracovat!",
//                     IsCompleted = true
//                 },
//                 new ToDoItem
//                 {
//                     ToDoItemId = 3,
//                     Name = "Streda",
//                     Description = "Odpocivat!",
//                     IsCompleted = true
//                 }
//             };

//             context.ToDoItems.AddRange(newItems);
//             context.SaveChanges();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.ReadById(2);
//             var okResult = result.Result as OkObjectResult;
//             var okResultValue = okResult.Value as ToDoItemReadResponseDto;

//             // Assert
//             Assert.Equal("Utery", okResultValue.Name);
//             Assert.Equal("Pracovat!", okResultValue.Description);
//             Assert.True(okResultValue.IsCompleted);
//         }

//         [Fact]
//         public void Get_ItemById_ReturnsNotFoundWhenNoItems()
//         {
//             // Arrange
//             var options = new DbContextOptionsBuilder<ToDoItemsContext>()
//                 .UseSqlite("Data Source=:memory:")
//                 .Options;

//             using var context = new ToDoItemsContext(options);
//             context.Database.OpenConnection(); // Needed for in-memory databases
//             context.Database.EnsureCreated();
//             var controller = new ToDoItemsController(context);

//             // Act
//             var result = controller.ReadById(1);
//             var noResult = result.Result as NotFoundResult;

//             // Assert
//             Assert.IsType<NotFoundResult>(noResult);
//         }
//     }
// }

// // When you're using ActionResult<T>, it combines two things:

// // Value: This is the actual content (data or object) that you're returning to the client (e.g., a list of items, an object, etc.).
// // Result: This represents the HTTP status code or response type, such as 200 OK, 404 Not Found, 400 Bad Request, etc.

// // result.Value: Here, you are trying to access the Value property of the ActionResult directly. However, the ActionResult type itself
// // doesn't have a Value property directly. The Value property is specific to result types like OkObjectResult.
// // Since result is of type ActionResult, attempting to access Value on it directly will not work unless the ActionResult is cast
// // to a specific type that includes the Value property, such as OkObjectResult.

// // Summary
// // ActionResult<T> allows returning both data and HTTP status, but to access Value, you need to ensure you are dealing with the specific result type that holds that property.
// // When returning from an action method, if you return an Ok(), the result is an OkObjectResult, but when you access it through the base type ActionResult, you need to check its actual type first.
// // In short, to access the Value, you always need to cast or check the result type to ensure you're dealing with an OkObjectResult.
// // If you directly access result.Value without confirming the type, it won't work, hence leading to a null reference or casting issue.
