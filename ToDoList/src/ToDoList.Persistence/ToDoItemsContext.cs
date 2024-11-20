namespace ToDoList.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using ToDoList.Domain.Models;

    public class ToDoItemsContext : DbContext
    {
        // Constructor accepts DbContextOptions and passes it to the base class
        public ToDoItemsContext(DbContextOptions<ToDoItemsContext> options)
            : base(options)
        {
            // Uncomment if you want to automatically apply migrations on startup
            this.Database.Migrate();
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        // This method can remain if you want to specify default configurations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) // Only configure if not already set
            {
                optionsBuilder.UseSqlite("Data Source=../../data/localdb.db");
            }
        }
    }
}
