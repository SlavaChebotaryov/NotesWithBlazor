using Microsoft.AspNetCore.Components.Web;
using NotesShare.Models;
using System.Net.Http.Json;

namespace NotesWithBlazor
{
	public partial class App
	{
		private int ComplitedNotesCounter = 0;

		private bool isEditing;
		private bool isInputDisabled;

		private Guid editingNoteId;

		private string? editedNoteText;
		private string? newNoteText;

		private List<Note> notes = new();

		protected override async Task OnInitializedAsync() => await LoadNotes();

		private void StartEditing(Note note)
		{
			if (note != null)
			{
				isEditing = true;
				editingNoteId = note.Id;
				editedNoteText = note.Text;
			}
		}

		private async Task AcceptCompletion(Note note)
		{
			note.IsComplited = true;
			await _httpClient.PutAsJsonAsync($"api/notes/{note.Id}", note);
			isEditing = false;
			await LoadNotes();
		}

		private async Task FinishEditing(Note note)
		{
			if (editingNoteId == note.Id && !String.IsNullOrEmpty(editedNoteText))
			{
				note.Text = editedNoteText;
				await _httpClient.PutAsJsonAsync($"api/notes/{note.Id}", note);
			}

			isEditing = false;
			await LoadNotes();
		}

		private async Task FinishEditingOnEnter(KeyboardEventArgs e, Note note)
		{
			if (e.Key == "Enter")
			{
				await FinishEditing(note);
				isEditing = false;
			}
		}
		private async Task DeleteCompletedNotes()
		{
			await _httpClient.DeleteAsync("api/notes/completed");
			ComplitedNotesCounter = 0;
			await LoadNotes();
		}

		private async Task HandleNewNoteInput(KeyboardEventArgs e)
		{
			if (e.Key == "Enter")
			{
				isInputDisabled = true;
				await AddNote();
				await LoadNotes();
				isInputDisabled = false;
				newNoteText = "";

			}
		}

		public async Task AddNote()
		{
			if (!String.IsNullOrEmpty(newNoteText))
				await _httpClient.PostAsJsonAsync("api/notes", new Note() { Id = Guid.NewGuid(), Text = newNoteText });
		}

		public async Task LoadNotes()
		{
			notes = await _httpClient.GetFromJsonAsync<List<Note>>("api/notes") ?? notes;
			StateHasChanged();

        }
	}
}
