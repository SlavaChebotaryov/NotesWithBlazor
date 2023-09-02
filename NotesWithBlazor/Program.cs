using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace NotesWithBlazor
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.RootComponents.Add<App>("#app");
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7073/") });
			
			var host = builder.Build();

			await host.RunAsync();


		}
	}
}