namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

public record class ToDoItemGetResponseDto(int ToDoItemId, string Name, string Description, string? Category, bool IsCompleted) //let client know the Id
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.ToDoItemId, item.Name, item.Description, item.Category, item.IsCompleted);
}
