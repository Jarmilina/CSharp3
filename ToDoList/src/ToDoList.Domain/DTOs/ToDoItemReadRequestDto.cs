namespace ToDoList.Domain.DTOs;

using ToDoList.Domain.Models;

public record ToDoItemReadRequestDto()
{
    public List<ToDoItem>? FromDomain() => new();
}
