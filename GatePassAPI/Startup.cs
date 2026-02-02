using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
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
using GatePassAPI.Repositories.EventRepository.Interfaces;
using GatePassAPI.Repositories.EventRepository;
using GatePassAPI.Finders.UserFinder.Interfaces;
using GatePassAPI.Finders.UserFinder;
using GatePassAPI.Factories.UserFactory.Interfaces;
using GatePassAPI.Factories.UserFactory;
using GatePassAPI.Repositories.UserRepository.Interfaces;
using GatePassAPI.Repositories.UserRepository;

namespace GatePassAPI;

public class Startup(IConfiguration configuration)
{
	public IConfiguration Configuration { get; } = configuration;

	public void ConfigureServices(IServiceCollection services)
    {
		var secretArn = Configuration["POSTGRES_SECRET_ARN"];
		
		var client = new AmazonSecretsManagerClient();
		var secretValue = client.GetSecretValueAsync(new GetSecretValueRequest
		{
			SecretId = secretArn
		}).Result;

		var secretJson = JsonDocument.Parse(secretValue.SecretString);
		var username = secretJson.RootElement.GetProperty("username").GetString();
		var password = secretJson.RootElement.GetProperty("password").GetString();

		var host = Configuration["DB_HOST"];
		var databaseName = Configuration["DB_NAME"];
		var port = Configuration["DB_PORT"];
	
		var connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={databaseName};Pooling=true";

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
				options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			});
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

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