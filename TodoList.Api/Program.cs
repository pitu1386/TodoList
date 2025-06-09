using TodoList.Application.Interfaces;
using TodoList.Application.Mappers;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Repositories;
using TodoListService = TodoList.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddSingleton<ITodoListRepository, TodoListRepository>();
builder.Services.AddSingleton<ITodoList, TodoListService.TodoList>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));



builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    if(context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
