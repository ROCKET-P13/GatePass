using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MotoPassAPI.Data;
using MotoPassAPI.Factories.TrackFactory;
using MotoPassAPI.Factories.TrackFactory.Interfaces;
using MotoPassAPI.Finders.TrackFinder;
using MotoPassAPI.Finders.TrackFinder.Interfaces;
using MotoPassAPI.Repositories.Interfaces;
using MotoPassAPI.Repositories.TrackRepository;

namespace MotoPassAPI;

public class LocalStartup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
    {
		string connectionString = Configuration.GetConnectionString("Postgres");
		Console.WriteLine("Connection String: " + connectionString);

		services.AddDbContext<AppDatabaseContext>(options =>
			options.UseNpgsql(connectionString));


		services.AddScoped<ITrackRepository, TrackRepository>();
		services.AddScoped<ITrackFinder, TrackFinder>();
		services.AddScoped<ITrackFactory, TrackFactory>();

		services.AddControllers()
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		});
    }

	public void Configure(IApplicationBuilder app)
    {

		app.UseDeveloperExceptionPage();

		using (var scope = app.ApplicationServices.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
			db.Database.Migrate();
		}

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("MotoPass API");
            });
        });
    }
}