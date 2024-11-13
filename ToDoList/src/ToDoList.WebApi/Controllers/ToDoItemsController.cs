namespace ToDoList.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    // private readonly ToDoItemsContext? context;
    private readonly IRepository<ToDoItem>? repository;

    public ToDoItemsController(IRepository<ToDoItem> repository)
    {
        this.repository = repository;
    }
    // public ToDoItemsController(ToDoItemsContext context)
    // {
    //     this.context = context;
    // }

    [HttpPost]
    public ActionResult<ToDoItemCreateRequestDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        try
        {
            repository.Create(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client: return Created(); //201
        return Created();
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemReadResponseDto>> Read()
    {
        try
        {
            var itemsDto = repository.Read();
            var response = itemsDto.Select(ToDoItemReadResponseDto.FromDomain).ToList();

            return Ok(response); //200
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemReadResponseDto> ReadById(int toDoItemId)
    {
        try
        {
            var item = repository.ReadById(toDoItemId);
            if (item == null)
            {
                return NotFound();
            }
            var response = ToDoItemReadResponseDto.FromDomain(item);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public ActionResult<ToDoItemUpdateResponseDto> UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var item = request.ToDomain();
        item.ToDoItemId = toDoItemId;

        try
        {
            var itemToUpdate = repository.UpdateById(item);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            var response = ToDoItemUpdateResponseDto.FromDomain(itemToUpdate);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public ActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = repository.DeleteById(toDoItemId);

            if (itemToDelete == null)
            {
                return NotFound();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
