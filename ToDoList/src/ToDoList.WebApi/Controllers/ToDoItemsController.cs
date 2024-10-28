namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    // public readonly List<ToDoItem> Items = []; // po dopsání úkolu již není potřeba a můžeme tento field smazat ;)
    private readonly ToDoItemsContext context;
    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    } //public only temporarily!

    //Od Pala
    // private static readonly List<ToDoItem> items = new List<ToDoItem>(){new()
    //         {
    //             ToDoItemId = 1,
    //             Name = "Pondeli",
    //             Description = "vstavat",
    //             IsCompleted = true
    //         }
    //     };

    // public static object ToDoItemId { get; private set; }

    [HttpPost]
    public ActionResult<ToDoItemCreateRequestDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            // item.ToDoItemId = Items.Count == 0 ? 1 : Items.Max(o => o.ToDoItemId) + 1;
            // Items.Add(item);

            context.ToDoItems.Add(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        //return Created(); //201
        return Created(); //201 //tato metoda z nějakého důvodu vrací status code No Content 204, zjištujeme proč ;)
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemReadResponseDto>> Read()
    {
        // var items = new List<ToDoItem>
        // {
        //     new ToDoItem
        //     {
        //         ToDoItemId = 1,
        //         Name = "Pondeli",
        //         Description = "vstavat",
        //         IsCompleted = true
        //     }
        // };

        try
        {
            var items = context.ToDoItems.ToList();

            var itemsDto = items.Select(item => new ToDoItemReadResponseDto
            {
                Name = item.Name,
                Description = item.Description,
                IsCompleted = item.IsCompleted
            }).ToList();

            return Ok(itemsDto); //200
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    //Z mainu od Vaska z lekce?
    // [HttpGet]
    // public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    // {
    //     List<ToDoItem> itemsToGet;
    //     try
    //     {
    //         itemsToGet = items;
    //     }
    //     catch (Exception ex)
    //     {
    //         return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
    //     }

    //     //respond to client
    //     return (itemsToGet is null)
    //         ? NotFound() //404
    //         : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    // }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemReadResponseDto> ReadById(int toDoItemId)
    {
        //try to read the item
        try
        {
            var items = context.ToDoItems.ToList();
            var item = items.Find(o => o.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }
            var response = ToDoItemReadResponseDto.FromDomain(item);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public ActionResult<ToDoItemUpdateResponseDto> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var requestItem = request.ToDomain();

        //try to find and update the item
        try
        {
            var items = context.ToDoItems.ToList();
            var itemToUpdate = items.Find(o => o.ToDoItemId == toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = requestItem.Name;
            itemToUpdate.Description = requestItem.Description;
            itemToUpdate.IsCompleted = requestItem.IsCompleted;
            context.SaveChanges();

            var response = ToDoItemUpdateResponseDto.FromDomain(itemToUpdate);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public ActionResult DeleteById(int toDoItemId)
    {
        var items = context.ToDoItems.ToList();
        ToDoItem? itemToDelete = new();

        //try to find and delete the item
        try
        {
            itemToDelete = items.Find(o => o.ToDoItemId == toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }
}
