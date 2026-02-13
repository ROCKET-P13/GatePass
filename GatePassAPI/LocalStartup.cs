using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using GatePassAPI.Data;
using GatePassAPI.Factories.VenueFactory;
using GatePassAPI.Factories.VenueFactory.Interfaces;
using GatePassAPI.Finders.VenueFinder;
using GatePassAPI.Finders.VenueFinder.Interfaces;
using GatePassAPI.Repositories.VenueRepository;
using GatePassAPI.Repositories.VenueRepository.Interfaces;
using GatePassAPI.Finders.EventFinder.Interfaces;
using GatePassAPI.Finders.EventFinder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GatePassAPI.Factories.EventFactory.Interfaces;
using GatePassAPI.Factories.EventFactory;
using GatePassAPI.Repositories.EventRepository;
using GatePassAPI.Repositories.EventRepository.Interfaces;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.UserFinder;
using GatePassAPI.Factories.UserFactory.Interfaces;
using GatePassAPI.Factories.UserFactory;
using GatePassAPI.Repositories.UserRepository.Interfaces;
using GatePassAPI.Repositories.UserRepository;
using System.Text.Json.Serialization;

namespace GatePassAPI;

public class LocalStartup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
    {
		string connectionString = Configuration.GetConnectionString("Postgres") ?? throw new Exception("Connection String not found");

		services.AddDbContext<AppDatabaseContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddScoped<IVenueRepository, VenueRepository>();
		services.AddScoped<IVenueFinder, VenueFinder>();
		services.AddScoped<IVenueFactory, VenueFactory>();

		services.AddScoped<IEventFinder, EventFinder>();
		services.AddScoped<IEventFactory, EventFactory>();
		services.AddScoped<IEventRepository, EventRepository>();

		services.AddScoped<IUserFinder, UserFinder>();
		services.AddScoped<IUserFactory, UserFactory>();
		services.AddScoped<IUserRepository, UserRepository>();

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
			options.Audience = Configuration["Auth0:Audience"];
			options.MapInboundClaims = false;
		});

		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

        app.UseRouting();
		app.UseAuthentication();
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