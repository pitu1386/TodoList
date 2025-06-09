using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.Application.Mappers;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Repositories;
using TodoListService = TodoList.Application.Services;

namespace TodoList.ConsoleApp;

public static class DependencyInjection
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<ITodoList, TodoListService.TodoList>();
        services.AddAutoMapper(typeof(AutoMapperProfile));
    }
}
