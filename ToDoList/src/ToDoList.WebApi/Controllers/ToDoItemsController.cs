namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> Items = [];

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
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = Items.Count == 0 ? 1 : Items.Max(o => o.ToDoItemId) + 1;
            Items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201
    }

    [HttpGet]
    public IActionResult Read()
    {
        var items = new List<ToDoItem>
        {
            new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Pondeli",
                Description = "vstavat",
                IsCompleted = true
            }
        };

        try
        {
            if (items == null)
            {
                return NotFound(); //404
            }

            var response = items.Select(item => ToDoItemReadResponseDto.FromDomain(item)).ToList();

            return Ok(response); //200
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        ToDoItem? item;

        //try to read the item
        try
        {
            item = Items.Find(o => o.ToDoItemId == toDoItemId);
            return (item == null) ? NotFound() : Ok(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var requestItem = request.ToDomain();
        ToDoItem? itemToUpdate;

        //try to find and update the item
        try
        {
            itemToUpdate = Items.Find(o => o.ToDoItemId == toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            //Tyto radky ted asi nejsou optimalni, protoze kdzy pouziju record, musim vyplnit vsechny properties.
            //TODO: Nastavit defaults? Nebo pouzit radeji class? Urcite nechci pri updatu vyplnovat vse.
            // itemToUpdate.Name = requestItem.Name ?? itemToUpdate.Name;
            // itemToUpdate.Description = requestItem.Description ?? itemToUpdate.Description;
            // itemToUpdate.IsCompleted = requestItem.IsCompleted ?? itemToUpdate.IsCompleted;

            return Ok(itemToUpdate);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        ToDoItem? itemToDelete = new();

        //try to find and delete the item
        try
        {
            itemToDelete = Items.Find(o => o.ToDoItemId == toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            Items.Remove(itemToDelete);

            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }
}

