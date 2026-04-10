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
using GatePassAPI.Finders.EventRegistrationFinder.Interfaces;
using GatePassAPI.Finders.EventRegistrationFinder;
using GatePassAPI.Finders.ParticipantFinder.Interfaces;
using GatePassAPI.Finders.ParticipantFinder;
using GatePassAPI.Factories.ParticipantFactory;
using GatePassAPI.Factories.ParticipantFactory.Interfaces;
using GatePassAPI.Repositories.ParticipantRepository.Interfaces;
using GatePassAPI.Repositories.ParticipantRepository;
using GatePassAPI.Factories.EventRegistrationFactory.Interfaces;
using GatePassAPI.Factories.EventRegistrationFactory;
using GatePassAPI.Repositories.EventRegistrationRepository.Interfaces;
using GatePassAPI.Repositories.EventRegistrationRepository;
using GatePassAPI.Factories.EventClassFactory.Interfaces;
using GatePassAPI.Factories.EventClassFactory;
using GatePassAPI.Repositories.EventClassRepository.Interfaces;
using GatePassAPI.Repositories.EventClassRepository;
using GatePassAPI.Finders.EventClassFinder.Interfaces;
using GatePassAPI.Finders.EventClassFinder;
using GatePassAPI.Finders.MotoFinder.Interfaces;
using GatePassAPI.Finders.MotoFinder;
using GatePassAPI.Factories.MotoFactory.Interfaces;
using GatePassAPI.Factories.MotoFactory;
using GatePassAPI.Repositories.MotoRepository.Interfaces;
using GatePassAPI.Repositories.MotoRepository;
using GatePassAPI.Repositories.MotoEntryRepository.Interfaces;
using GatePassAPI.Repositories.MotoEntryRepository;

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

		services.AddScoped<IEventRegistrationFinder, EventRegistrationFinder>();
		services.AddScoped<IEventRegistrationFactory, EventRegistrationFactory>();
		services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();

		services.AddScoped<IParticipantFinder, ParticipantFinder>();
		services.AddScoped<IParticipantFactory, ParticipantFactory>();
		services.AddScoped<IParticipantRepository, ParticipantRepository>();

		services.AddScoped<IEventClassFinder, EventClassFinder>();
		services.AddScoped<IEventClassFactory, EventClassFactory>();
		services.AddScoped<IEventClassRepository, EventClassRepository>();

		services.AddScoped<IMotoFinder, MotoFinder>();
		services.AddScoped<IMotoFactory, MotoFactory>();
		services.AddScoped<IMotoRepository, MotoRepository>();

		services.AddScoped<IMotoEntryRepository, MotoEntryRepository>();

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