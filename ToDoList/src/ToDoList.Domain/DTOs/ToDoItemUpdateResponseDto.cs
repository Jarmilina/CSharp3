using ToDoList.Domain.Models;

namespace ToDoList.Domain.DTOs
{
    public class ToDoItemUpdateResponseDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string? Category { get; set; }
        public bool IsCompleted { get; set; }

        public static ToDoItemUpdateResponseDto FromDomain(ToDoItem item)
        {
            return new ToDoItemUpdateResponseDto
            {
                Name = item.Name,
                Description = item.Description,
                Category = item.Category,
                IsCompleted = item.IsCompleted,
            };
        }

    }
}
