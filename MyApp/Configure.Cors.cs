[assembly: HostingStartup(typeof(MyApp.ConfigureCors))]

namespace MyApp;

public class ConfigureCors : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins([
                        "http://localhost:5000", "https://localhost:5001", "http://localhost:8080",
                        "https://localhost:5173", "http://localhost:5173",
                        "https://docs.servicestack.net","https://press-react.servicestack.net",
                        "https://react.servicestack.net",
                    ])
                    .AllowCredentials()
                    .WithHeaders(["Content-Type", "Allow", "Authorization"])
                    .SetPreflightMaxAge(TimeSpan.FromHours(1));
                });
            });
            services.AddTransient<IStartupFilter, StartupFilter>();
        });

    public class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            app.UseCors();
            next(app);
        };
    }        
}
