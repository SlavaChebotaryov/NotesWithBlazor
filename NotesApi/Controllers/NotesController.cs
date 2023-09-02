using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotesApi.Data;
using NotesShare.Models;

namespace NotesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly ILogger<NotesController> _logger;

		public NotesController(AppDbContext context, ILogger<NotesController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<List<Note>> GetNotes()
		{
			_logger.LogInformation("GetNotes.");

			return await Task.FromResult(_context.Notes.OrderByDescending(note => note.CreatedAt).Take(10).ToList());
		}

		[HttpPost]
		public IActionResult Create(Note note)
		{
			_logger.LogInformation("CreateNote.");

			note.Id = Guid.NewGuid();
			_context.Notes.Add(note);
			_context.SaveChanges();
			return Ok();
		}
		[HttpPut("{id}")]
		public IActionResult Update(Guid id, Note updatedNote)
		{
			_logger.LogInformation("UpdateNotes.");
			var existingNote = _context.Notes.Find(id);
			if (existingNote == null)
			{
				return NotFound();
			}

			_context.Entry(existingNote).CurrentValues.SetValues(updatedNote);
			_context.SaveChanges();

			return Ok();

		}
		[HttpDelete("completed")]
		public IActionResult DeleteCompletedNotes()
		{
			_logger.LogInformation("DeleteNotes");
			var completedNotes = _context.Notes.Where(note => note.IsComplited).ToList();

			if (completedNotes.Count == 0)
			{
				return NoContent();
			}

			_context.Notes.RemoveRange(completedNotes);
			_context.SaveChanges();

			return Ok();
		}
	}
}