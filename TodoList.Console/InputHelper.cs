namespace TodoList.ConsoleApp;

public static class InputHelper
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
                return result;
            Console.WriteLine("Please enter a valid number.");
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal result))
                return result;
            Console.WriteLine("Please enter a valid decimal number.");
        }
    }

    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime result))
                return result;
            Console.WriteLine("Please enter a valid date (example: 2025-06-06).");
        }
    }

    public static string ChooseFromList(string prompt, List<string> options)
    {
        Console.WriteLine(prompt);
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {options[i]}");
        }

        int selectedIndex;
        while (true)
        {
            Console.Write("Select an option: ");
            if (int.TryParse(Console.ReadLine(), out selectedIndex) && selectedIndex >= 1 && selectedIndex <= options.Count)
                return options[selectedIndex - 1];

            Console.WriteLine("Invalid option.");
        }
    }


}
