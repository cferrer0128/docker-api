using Microsoft.EntityFrameworkCore;
using docker_api.Data;
using docker_api.Entity;


namespace docker_api.NotesRepository
{
    public class NotesRepository : INotesRepository
    {
        private readonly DataContext _dataContext;
        public NotesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            var noteModels = await _dataContext.noteModels.AddAsync(note);
            noteModels.Context.SaveChanges();
            return note;

        }

        public async Task<NoteModel> GetNoteById(int Id)
        {
            var _note =  await _dataContext.noteModels.FindAsync(Id);
          
            return _note!;
        }

        public async Task<IEnumerable<NoteModel>> GetAllNotes()
        {
            var noteModels = await _dataContext.noteModels.ToListAsync();
            return noteModels;
        }

        
    }
}
