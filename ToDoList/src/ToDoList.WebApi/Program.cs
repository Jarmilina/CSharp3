using ToDoList.Persistence;

var builder = WebApplication.CreateBuilder(args);

{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ToDoItemsContext>();
}

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();
{
    //Configure Middleware (HTTP request pipeline)
    app.MapControllers();
}

app.MapGet("/", () => "Ahoj Jaris!");


app.Run();


