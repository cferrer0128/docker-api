using docker_api.Entity;

namespace docker_api.NotesRepository
{
   
    public interface INotesRepository
    {
        Task<IEnumerable<NoteModel>> GetAllNotes();
        Task<NoteModel> GetNoteById(int Id);
        Task<NoteModel> AddNote(NoteModel note);
    }
}
