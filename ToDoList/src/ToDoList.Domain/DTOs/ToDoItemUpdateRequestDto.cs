namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(int ToDoItemId, string? Name, string? Description, bool? IsCompleted)
{
    public ToDoItem ToDomain() => new()
    {
        ToDoItemId = ToDoItemId,
        Name = Name,
        Description = Description,
        IsCompleted = IsCompleted,
    };
}
