using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Clients;
using ToDoList.Frontend.Views;

public class ToDoItemsClient(HttpClient httpClient) : IToDoItemsClient
{

    public async Task<List<ToDoItemView>> ReadItemsAsync()
    {
        var toDoItemsView = new List<ToDoItemView>();
        var respose = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");

        toDoItemsView = respose.Select(dto => new ToDoItemView
        {
            ToDoItemId = dto.ToDoItemId,
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            IsCompleted = dto.IsCompleted
        }).ToList();

        return toDoItemsView;
    }
}
