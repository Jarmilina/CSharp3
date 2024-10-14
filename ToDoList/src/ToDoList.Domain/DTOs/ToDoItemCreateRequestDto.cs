namespace ToDoList.Domain.DTOs
{
    using ToDoList.Domain.Models;

    // public class ToDoItemCreateRequestDto hure serializovatelne
    public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted) //lehceji serializovatelne a musis vzdycky vyplnit vsechny - hmm, oh no - a nepotrebuje properties
    {
        public ToDoItem ToDomain() => new ToDoItem
        {
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted,
        };
    }
}
