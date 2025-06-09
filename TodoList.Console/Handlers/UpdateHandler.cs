using TodoList.Application.Interfaces;

namespace TodoList.ConsoleApp.Handlers;

public static class UpdateHandler
{
    public static void Handle(ITodoList todoList)
    {
        var id = InputHelper.ReadInt("ID: ");

        Console.Write("New Description: ");
        var desc = Console.ReadLine()!;

        todoList.UpdateItem(id, desc);

        Console.WriteLine("Item updated successfully.");
    }
}