using System.Text.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.EntityFrameworkCore;
using MotoPassAPI.Data;
using MotoPassAPI.Entities;
using MotoPassAPI.Factories.TrackFactory;
using MotoPassAPI.Factories.TrackFactory.Interfaces;
using MotoPassAPI.Finders.TrackFinder;
using MotoPassAPI.Finders.TrackFinder.Interfaces;
using MotoPassAPI.Repositories.Interfaces;
using MotoPassAPI.Repositories.TrackRepository;

namespace MotoPassAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        // services.AddControllers();
		var secretArn = Configuration["POSTGRES_SECRET_ARN"];

		string connectionString = "";

		if (!string.IsNullOrEmpty(secretArn))
		{
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
		
			connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={databaseName};Pooling=true";
		} else {
			connectionString = Configuration.GetConnectionString("Postgres") ?? "";
		}


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

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
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