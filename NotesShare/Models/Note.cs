namespace NotesShare.Models
{
	public class Note
	{
		public Guid Id { get; set; }
		public string Text { get; set; } = "";
		public bool IsComplited { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.Now;


	}
}
