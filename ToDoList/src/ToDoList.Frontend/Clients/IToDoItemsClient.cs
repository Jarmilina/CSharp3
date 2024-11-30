using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Frontend.Views;

namespace ToDoList.Frontend.Clients
{
    public interface IToDoItemsClient
    {
        public Task CreateItemAsync(ToDoItemView itemView);
        public Task<ToDoItemView?> ReadItemByIdAsync(int toDoItemId);
        public Task<List<ToDoItemView>> ReadItemsAsync();
        public Task DeleteItemAsync(ToDoItemView itemView);
        public Task UpdateItemAsync(ToDoItemView itemView);
    }
}
