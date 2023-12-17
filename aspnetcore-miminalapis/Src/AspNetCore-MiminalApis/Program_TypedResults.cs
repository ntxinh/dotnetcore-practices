// using AspNetCoreMiminalApis;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>();
// // builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // Minimal APIs with MapGroup
// var todoItems = app.MapGroup("/todoitems");

// todoItems.MapGet("/", GetAllTodos);
// todoItems.MapGet("/complete", GetCompleteTodos);
// todoItems.MapGet("/{id}", GetTodo);
// todoItems.MapPost("/", CreateTodo);
// todoItems.MapPut("/{id}", UpdateTodo);
// todoItems.MapDelete("/{id}", DeleteTodo);

// // Default Minimal APIs
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// // Returning TypedResults rather than Results has several advantages, including testability and automatically returning the response type metadata for OpenAPI to describe the endpoint

// static async Task<IResult> GetAllTodos(ApplicationDbContext db)
// {
//     return TypedResults.Ok(await db.Todos.ToArrayAsync());
// }

// static async Task<IResult> GetCompleteTodos(ApplicationDbContext db)
// {
//     return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).ToListAsync());
// }

// static async Task<IResult> GetTodo(int id, ApplicationDbContext db)
// {
//     return await db.Todos.FindAsync(id)
//         is Todo todo
//             ? TypedResults.Ok(todo)
//             : TypedResults.NotFound();
// }

// static async Task<IResult> CreateTodo(Todo todo, ApplicationDbContext db)
// {
//     db.Todos.Add(todo);
//     await db.SaveChangesAsync();

//     return TypedResults.Created($"/todoitems/{todo.Id}", todo);
// }

// static async Task<IResult> UpdateTodo(int id, Todo inputTodo, ApplicationDbContext db)
// {
//     var todo = await db.Todos.FindAsync(id);

//     if (todo is null) return TypedResults.NotFound();

//     todo.Name = inputTodo.Name;
//     todo.IsComplete = inputTodo.IsComplete;

//     await db.SaveChangesAsync();

//     return TypedResults.NoContent();
// }

// static async Task<IResult> DeleteTodo(int id, ApplicationDbContext db)
// {
//     if (await db.Todos.FindAsync(id) is Todo todo)
//     {
//         db.Todos.Remove(todo);
//         await db.SaveChangesAsync();
//         return TypedResults.NoContent();
//     }

//     return TypedResults.NotFound();
// }

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }