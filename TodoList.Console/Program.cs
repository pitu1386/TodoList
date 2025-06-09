using Microsoft.Extensions.DependencyInjection;
using TodoList.ConsoleApp;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        DependencyInjection.Configure(services);
        var provider = services.BuildServiceProvider();

        var app = new App(provider);
        app.Run();
    }
}
