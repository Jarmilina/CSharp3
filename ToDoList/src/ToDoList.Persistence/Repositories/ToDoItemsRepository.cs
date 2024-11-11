
namespace ToDoList.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using ToDoList.Domain.DTOs;
    using ToDoList.Domain.Models;

    public class ToDoItemsRepository : IRepository<ToDoItem>
    {
        private readonly ToDoItemsContext context;

        public ToDoItemsRepository(ToDoItemsContext context)
        {
            this.context = context;
        }

        public void Create(ToDoItem item)
        {
            context.ToDoItems.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<ToDoItem> Read()
        {
            var todoItems = context.ToDoItems;

            return todoItems;
        }

        public ToDoItem? ReadById(int itemId)
        {
            var toDoItem = context.ToDoItems.Find(itemId);

            return toDoItem;
        }

        public ToDoItem? UpdateById(ToDoItem item)
        {
            // var items = context.ToDoItems.ToList();
            var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId);

            if (itemToUpdate == null)
            {
                return itemToUpdate;
            }

            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;
            context.SaveChanges();

            return itemToUpdate;
        }

        public ToDoItem? DeleteById(int itemId)
        {
            var items = context.ToDoItems;
            var itemToDelete = items.Find(itemId);

            if (itemToDelete == null)
            {
                return null;
            }

            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();

            return itemToDelete;
        }
    }
}
