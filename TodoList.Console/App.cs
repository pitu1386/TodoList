using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces;
using TodoList.ConsoleApp.Handlers;
using TodoList.Domain.Interfaces;

namespace TodoList.ConsoleApp;

public class App
{
    private readonly ITodoList _todoList;
    private readonly ITodoListRepository _repository;

    public App(IServiceProvider provider)
    {
        _todoList = provider.GetRequiredService<ITodoList>();
        _repository = provider.GetRequiredService<ITodoListRepository>();
    }


    public void Run()
    {
        while (true)
        {
            Menu.Show();
            var option = Console.ReadLine();
            switch (option)
            {
                case "1": CreateHandler.Handle(_todoList, _repository); break;
                case "2": UpdateHandler.Handle(_todoList); break;
                case "3": RegisterProgressionHandler.Handle(_todoList); break;
                case "4": _todoList.PrintItems(); Console.ReadKey(); break;
                case "0": return;
                default: Console.WriteLine("Invalid option"); break;
            }
        }
    }
}