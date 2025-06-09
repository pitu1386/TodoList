namespace TodoList.ConsoleApp;

public static class Menu
{
    public static void Show()
    {
        Console.Clear();
        Console.WriteLine("Todo List Console App");
        Console.WriteLine("1 - Create Todo Item");
        Console.WriteLine("2 - Update Description");
        Console.WriteLine("3 - Register Progression");
        Console.WriteLine("4 - Print All Items");
        Console.WriteLine("0 - Exit");
        Console.Write("Select an option: ");
    }
}
