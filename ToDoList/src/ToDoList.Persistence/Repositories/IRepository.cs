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

    public interface IRepositoryAsync<T> where T : class
    {
        public Task CreateAsync(T item);

        public Task<IEnumerable<T>> ReadAsync();

        public Task<T> ReadByIdAsync(int itemId);

        public Task<T?> UpdateByIdAsync(T item);

        public Task<T?> DeleteByIdAsync(int itemId);
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
