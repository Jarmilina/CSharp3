var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/nazdarSvete", () => "Nazdar světe!");


app.MapGet("/randomKocka", () => [
    {
        "id": "62v",
        "url": "https://cdn2.thecatapi.com/images/62v.jpg",
        "width": 960,
        "height": 720
    }
]);

app.Run();
