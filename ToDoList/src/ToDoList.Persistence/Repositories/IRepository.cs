namespace ToDoList.Persistence.Repositories
{
    using ToDoList.Domain.DTOs;

    public interface IRepository<T> where T : class
    {
        public void Create(T item);

        public IEnumerable<T> Read();

        public T ReadById(int itemId);

        public T? UpdateById(T item);

        public T? DeleteById(int itemId);
    }
}


//     public interface IRepositoryForToDoItem<T> where T : class
//     {
//         public void Create(T item);
//     }

//     public interface IRepository<T> where T : class // generic interface
//     {
//         public void Create(T item);
//     }
