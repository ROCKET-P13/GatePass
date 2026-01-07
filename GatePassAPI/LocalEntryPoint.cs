namespace GatePassAPI;

/// <summary>
/// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
/// </summary>
public class LocalEntryPoint
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppConfiguration((context, config) =>
				{
					config
					.AddJsonFile(
						$"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
						optional: true
					);
				}).UseStartup<LocalStartup>();
            });
	}
}
