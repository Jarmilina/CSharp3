
namespace ToDoList.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using ToDoList.Domain.DTOs;
    using ToDoList.Domain.Models;

    public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
    {
        private readonly ToDoItemsContext context;

        public ToDoItemsRepository(ToDoItemsContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(ToDoItem item)
        {
            await context.ToDoItems.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> ReadAsync()
        {
            var todoItems = context.ToDoItems;

            return await todoItems.ToListAsync();
        }

        public async Task<ToDoItem?> ReadByIdAsync(int itemId)
        {
            var toDoItem = await context.ToDoItems.FindAsync(itemId);

            return toDoItem;
        }

        public async Task<ToDoItem?> UpdateByIdAsync(ToDoItem item)
        {
            // var items = context.ToDoItems.ToList();
            var itemToUpdate = await context.ToDoItems.FindAsync(item.ToDoItemId);

            if (itemToUpdate == null)
            {
                return itemToUpdate;
            }

            // itemToUpdate.Name = item.Name;
            // itemToUpdate.Description = item.Description;
            // itemToUpdate.Category = item.Category;
            // itemToUpdate.IsCompleted = item.IsCompleted;
            context.Entry(itemToUpdate).CurrentValues.SetValues(item);

            await context.SaveChangesAsync();

            return itemToUpdate;
        }

        public async Task<ToDoItem?> DeleteByIdAsync(int itemId)
        {
            var itemToDelete = await context.ToDoItems.FindAsync(itemId);

            if (itemToDelete == null)
            {
                return null;
            }

            context.ToDoItems.Remove(itemToDelete);
            await context.SaveChangesAsync();

            return itemToDelete;
        }
    }
}
