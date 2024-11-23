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
    private readonly IRepositoryAsync<ToDoItem>? repository;

    public ToDoItemsController(IRepositoryAsync<ToDoItem> repository)
    {
        this.repository = repository;
    }
    // public ToDoItemsController(ToDoItemsContext context)
    // {
    //     this.context = context;
    // }

    [HttpPost]
    public async Task<ActionResult<ToDoItemCreateRequestDto>> CreateAsync(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        try
        {
            await repository.CreateAsync(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client: return Created(); //201
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemReadResponseDto>>> ReadAsync()
    {
        try
        {
            var itemsDto = await repository.ReadAsync();
            var response = itemsDto.Select(ToDoItemReadResponseDto.FromDomain).ToList();

            return Ok(response); //200
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public async Task<ActionResult<ToDoItemReadResponseDto>> ReadByIdAsync(int toDoItemId)
    {
        try
        {
            var item = await repository.ReadByIdAsync(toDoItemId);
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
    public async Task<ActionResult<ToDoItemUpdateResponseDto>> UpdateByIdAsync(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var item = request.ToDomain();
        item.ToDoItemId = toDoItemId;

        try
        {
            var itemToUpdate = await repository.UpdateByIdAsync(item);

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
    public async Task<ActionResult> DeleteByIdAsync(int toDoItemId)
    {
        try
        {
            var itemToDelete = await repository.DeleteByIdAsync(toDoItemId);

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
