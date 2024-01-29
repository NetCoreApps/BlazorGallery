using Funq;
using MyApp.ServiceInterface;
using MyApp.ServiceModel;

[assembly: HostingStartup(typeof(MyApp.AppHost))]

namespace MyApp;

public class AppHost() : AppHostBase("MyApp", typeof(MyServices).Assembly), IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) =>
        {
            // Configure ASP.NET Core IOC Dependencies
        });

    // Configure your AppHost with the necessary configuration and dependencies your App needs
    public override void Configure(Container container)
    {
        SetConfig(new HostConfig
        {
        });

        ScriptContext.Args[nameof(AppData)] = new AppData
        {
            Currencies = NumberCurrency.All,
            AlphaValues = ["Alpha", "Bravo", "Charlie"],
            AlphaDictionary = new()
            {
                ["A"] = "Alpha",
                ["B"] = "Bravo",
                ["C"] = "Charlie",
            },
            AlphaKeyValuePairs = new()
            {
                new("A", "Alpha"),
                new("B", "Bravo"),
                new("C", "Charlie"),
            },
        };
    }
}