namespace NotesWithBlazor.Models
{
	public class Note
	{
		public Guid Id { get; set; }
		public string? Text { get; set; }
		public bool IsComplited { get; set; } = false;

    }
}
