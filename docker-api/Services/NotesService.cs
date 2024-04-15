using docker_api.Entity;
using docker_api.NotesRepository;

namespace docker_api.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _serviceRepository;
        public NotesService(INotesRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;

        }
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            return await _serviceRepository.AddNote(note);
            
        }

        public async Task<IEnumerable<NoteModel>> GetAllNotes()
        {
            return await _serviceRepository.GetAllNotes();
        }

        public async Task<NoteModel> GetNoteById(int Id)
        {
            return await _serviceRepository.GetNoteById(Id);
        }
    }
}
