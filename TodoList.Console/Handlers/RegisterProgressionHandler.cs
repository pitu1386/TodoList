using TodoList.Application.Interfaces;
using TodoList.Shared.Dtos.Progression;

namespace TodoList.ConsoleApp.Handlers;

public static class RegisterProgressionHandler
{
    public static void Handle(ITodoList todoList)
    {
        var id = InputHelper.ReadInt("TodoItem ID: ");
        var date = InputHelper.ReadDate("Date (yyyy-MM-dd): ");
        decimal percent;

        while (true)
        {
            percent = InputHelper.ReadDecimal("Percent (1-100): ");

            if (percent <= 0 || percent > 100)
            {
                Console.WriteLine("Error: The percentage must be between 1 and 100.");
            }
            else
            {
                break;
            }
        }

        var dto = new RegisterProgressionDto { TodoItemId = id, Date = date, Percent = percent };

        try
        {
            todoList.RegisterProgression(dto);
            Console.WriteLine("Progression successfully recorded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error recording progression: {ex.Message}");
        }

        Console.ReadKey();


    }

}

