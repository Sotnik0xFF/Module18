using Microsoft.Extensions.DependencyInjection;
using Module18.FinalPractice.UI;

namespace Module18.FinalPractice;

internal class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<IYouTubeLoaderService>().Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IYouTubeLoaderService, YouTubeLoaderService>();
        services.AddSingleton<IUserInterface, ConsoleUserInterface>();
    }
}