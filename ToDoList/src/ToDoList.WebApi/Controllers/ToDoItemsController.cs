namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public List<ToDoItem> Items = []; //public only temporarily!

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
            item.ToDoItemId = Items.Count == 0 ? 1 : Items.Max(o => o.ToDoItemId) + 1;
            Items.Add(item);
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
        var items = Items;

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
            var item = Items.Find(o => o.ToDoItemId == toDoItemId);
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
            var itemToUpdate = Items.Find(o => o.ToDoItemId == toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            //Tyto radky ted asi nejsou optimalni, protoze kdzy pouziju record, musim vyplnit vsechny properties.
            //TODO: Nastavit defaults? Nebo pouzit radeji class? Urcite nechci pri updatu vyplnovat vse.
            // itemToUpdate.Name = requestItem.Name ?? itemToUpdate.Name;
            // itemToUpdate.Description = requestItem.Description ?? itemToUpdate.Description;
            // itemToUpdate.IsCompleted = requestItem.IsCompleted ?? itemToUpdate.IsCompleted;\

            // var itemIndexToUpdate = Items.FindIndex(i => i.ToDoItemId == toDoItemId);
            // if (itemIndexToUpdate == -1)
            // {
            //     return NotFound(); //404
            // }
            // itemToUpdate.ToDoItemId = toDoItemId;
            // Items[itemIndexToUpdate] = itemToUpdate;
            itemToUpdate.Name = requestItem.Name;
            itemToUpdate.Description = requestItem.Description;
            itemToUpdate.IsCompleted = requestItem.IsCompleted;

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
