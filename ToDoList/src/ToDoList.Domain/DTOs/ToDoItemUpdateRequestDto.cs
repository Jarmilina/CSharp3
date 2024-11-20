namespace ToDoList.Domain.DTOs;

using System.ComponentModel;
using ToDoList.Domain.Models;
using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(string Name, string Description, string? Category, bool IsCompleted)
{
    public ToDoItem ToDomain() => new()
    {
        Name = Name,
        Description = Description,
        Category = Category,
        IsCompleted = IsCompleted,
    };
}
