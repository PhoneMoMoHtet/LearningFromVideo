using VideoLearning;
using VideoLearning.Components;

internal class Program
{
    private static IConfigurationRoot config;

    private static void Main(string[] args)
    {
        Initialize();

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddScoped<ProgramParameter>(provider =>
        {
            // Crea un'istanza di EmployeeRepository utilizzando la connessione dal file di configurazione.
            return new ProgramParameter(config.GetConnectionString("DefaultConnection"),
                config.GetConnectionString("PubsConnection"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }

    private static void Initialize()
    {
        var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config = builder.Build();
    }
}