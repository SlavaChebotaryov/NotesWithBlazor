using Microsoft.EntityFrameworkCore;
using NotesShare.Models;

namespace NotesApi.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Note> Notes { get; set; }
		public AppDbContext() => Database.EnsureCreated();

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Notes.db");
			optionsBuilder.UseSqlite($"Data Source={dbPath}");
		}
	}
}
