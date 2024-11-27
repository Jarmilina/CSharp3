namespace ToDoList.Domain.DTOs;

using System.ComponentModel;
using ToDoList.Domain.Models;

public class ToDoItemReadResponseDto
{
    public int ToDoItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Category { get; set; }
    public bool IsCompleted { get; set; }

    public static ToDoItemReadResponseDto FromDomain(ToDoItem item)
    {
        return new ToDoItemReadResponseDto
        {
            ToDoItemId = item.ToDoItemId,
            Name = item.Name,
            Description = item.Description,
            Category = item.Category,
            IsCompleted = item.IsCompleted,
        };
    }
}


// public record ToDoItemReadResponseDto()
// {
//     public List<ToDoItem>? FromDomain() => new();
// }
