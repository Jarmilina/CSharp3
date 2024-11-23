using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

{
    //Configure DI
    //WebApi services
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();

    //Persistence services
    builder.Services.AddDbContext<ToDoItemsContext>();
    builder.Services.AddScoped<IRepositoryAsync<ToDoItem>, ToDoItemsRepository>();
}

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();
{
    //Configure Middleware (HTTP request pipeline)
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1"));
}

app.MapGet("/", () => "Ahoj Jaris!");


app.Run();


