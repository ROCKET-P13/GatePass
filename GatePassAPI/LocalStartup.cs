using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Factories.VenueFactory;
using GatePassAPI.Factories.VenueFactory.Interfaces;
using GatePassAPI.Finders.VenueFinder;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.VenueRepository;
using GatePassAPI.Repositories.VenueRepository.Interfaces;

namespace GatePassAPI;

public class LocalStartup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
    {
		string connectionString = Configuration.GetConnectionString("Postgres") ?? throw new Exception("Connection String not found");
		Console.WriteLine("Connection String: " + connectionString);

		services.AddDbContext<AppDatabaseContext>(options =>
			options.UseNpgsql(connectionString));


		services.AddScoped<IVenueRepository, VenueRepository>();
		services.AddScoped<IVenueFinder, VenueFinder>();
		services.AddScoped<IVenueFactory, VenueFactory>();

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
                await context.Response.WriteAsync("GatePass API");
            });
        });
    }
}