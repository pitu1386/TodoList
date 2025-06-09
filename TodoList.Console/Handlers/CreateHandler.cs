using TodoList.Application.Interfaces;
using TodoList.Domain.Interfaces;
using TodoList.Shared.Dtos.TodoItem;

namespace TodoList.ConsoleApp.Handlers;

public static class CreateHandler
{
    public static void Handle(ITodoList todoList, ITodoListRepository repository)
    {
        var id = InputHelper.ReadInt("ID: ");

        Console.Write("Title: ");
        var title = Console.ReadLine()!;

        Console.Write("Description: ");
        var desc = Console.ReadLine()!;

        // Mostrar categorías como combo simulado
        var categories = repository.GetAllCategories(); // o desde el repositorio si lo pasás
        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {categories[i]}");
        }

        int selectedIndex = -1;
        while (selectedIndex < 1 || selectedIndex > categories.Count)
        {
            Console.Write("Select a category: ");
            if (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 1 || selectedIndex > categories.Count)
                Console.WriteLine("Invalid option. Please try again.");
        }

        string selectedCategory = categories[selectedIndex - 1];

        var dto = new CreateTodoItemDto
        {
            Id = id,
            Title = title,
            Description = desc,
            Category = selectedCategory
        };

        todoList.AddItem(dto);
        Console.Write("Item added successfully.");
    }
}