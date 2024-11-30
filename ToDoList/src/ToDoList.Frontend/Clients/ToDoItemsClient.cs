namespace ToDoList.Frontend.Clients;

using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Views;

public class ToDoItemsClient(HttpClient httpClient) : IToDoItemsClient //primary constructor
{
    public async Task CreateItemAsync(ToDoItemView itemView)
    {
        var itemRequest = new ToDoItemCreateRequestDto(itemView.Name, itemView.Description, itemView.Category, itemView.IsCompleted);
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/ToDoItems", itemRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine($"POST request successful: Created a ToDoItem.");
                return;
            }
            else
            {
                Console.WriteLine($"POST request failed with status code: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occured: {e.Message}");
        }
    }

    public async Task DeleteItemAsync(ToDoItemView itemView)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/ToDoItems/{itemView.ToDoItemId}");
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine($"DELETE request successful: Deleted ToDoItem with id {itemView.ToDoItemId}.");
                return;
            }
            else
            {
                Console.WriteLine($"DELETE request failed with status code: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occured: {e.Message}");
        }
    }

    public async Task<ToDoItemView?> ReadItemByIdAsync(int toDoItemId)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<ToDoItemGetResponseDto>($"api/TodoItems/{toDoItemId}");
            if (response is null)
            {
                Console.WriteLine($"GET request failed: Item with {toDoItemId} id not found.");
                throw new ArgumentException($"Given id {toDoItemId} does not exist.");
            }

            return new()
            {
                ToDoItemId = response.ToDoItemId,
                Name = response.Name,
                Description = response.Description,
                Category = response.Category,
                IsCompleted = response.IsCompleted,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occured: {e.Message}");
            return null;
        }
    }

    public async Task<List<ToDoItemView>> ReadItemsAsync()
    {
        var toDoItemViews = new List<ToDoItemView>();
        try
        {
            var response = await httpClient.GetFromJsonAsync<List<ToDoItemReadResponseDto>>("api/ToDoItems");
            if (response is null)
            {
                Console.WriteLine($"GET request failed: No items to read.");
                return toDoItemViews;
            }
            toDoItemViews = response.Select(dto => new ToDoItemView
            {
                ToDoItemId = dto.ToDoItemId,
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                IsCompleted = dto.IsCompleted
            }).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occured: {e.Message}");
        }

        return toDoItemViews;
    }
    public async Task UpdateItemAsync(ToDoItemView itemView)
    {
        try
        {
            var itemRequest = new ToDoItemUpdateRequestDto(itemView.Name, itemView.Description, itemView.Category, itemView.IsCompleted);
            var response = await httpClient.PutAsJsonAsync($"api/ToDoItems/{itemView.ToDoItemId}", itemRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine($"PUT request successful: Updated ToDoItem with id {itemView.ToDoItemId}.");
                return;
            }
            else
            {
                Console.WriteLine($"PUT request failed: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception occured: {e.Message}");
        }
    }
}

// using System.Security.Cryptography.X509Certificates;
// using Microsoft.AspNetCore.Authorization.Infrastructure;
// using ToDoList.Domain.DTOs;
// using ToDoList.Frontend.Clients;
// using ToDoList.Frontend.Views;

// public class ToDoItemsClient(HttpClient httpClient) : IToDoItemsClient
// {

//     public async Task<List<ToDoItemView>> ReadItemsAsync()
//     {
//         var toDoItemsView = new List<ToDoItemView>();
//         var respose = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");

//         toDoItemsView = respose.Select(dto => new ToDoItemView
//         {
//             ToDoItemId = dto.ToDoItemId,
//             Name = dto.Name,
//             Description = dto.Description,
//             Category = dto.Category,
//             IsCompleted = dto.IsCompleted
//         }).ToList();

//         return toDoItemsView;
//     }
// }
