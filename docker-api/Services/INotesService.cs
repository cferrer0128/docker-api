using docker_api.Entity;

namespace docker_api.Services
{
    
    public interface INotesService
    {
        Task<IEnumerable<NoteModel>> GetAllNotes();
        Task<NoteModel> GetNoteById(int Id);
        Task<NoteModel> AddNote(NoteModel note);
    }
}
