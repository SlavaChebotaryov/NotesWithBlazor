using NotesApi.Data;
using Microsoft.Extensions.Logging;

namespace NotesApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddLogging(builder =>
			{
				builder.AddConsole(o=>o.LogToStandardErrorThreshold = LogLevel.Information);
			});
			builder.Services.AddDbContext<AppDbContext>();
			builder.Services.AddControllers();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAnyOrigin",
					builder =>
					{
						builder.AllowAnyOrigin()
							   .AllowAnyHeader()
							   .AllowAnyMethod();
					});
			});

			var app = builder.Build();
			app.UseCors("AllowAnyOrigin");

			app.MapControllers();

			app.Run();
		}
	}
}