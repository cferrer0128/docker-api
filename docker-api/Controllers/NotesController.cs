using Microsoft.AspNetCore.Mvc;
using docker_api.Entity;
using docker_api.Services;

namespace docker_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly INotesService _notesService;

        public NotesController(ILogger<NotesController> logger,
            INotesService notesService)
        {
            _logger = logger;
            _notesService = notesService;

        }
        [HttpGet(Name = "notes")]
        public async Task<IEnumerable<NoteModel>> Get()
        {
            var result = await _notesService.GetAllNotes();
            return result;
        }
        [HttpPost(Name = "addnote")]
        public async Task<NoteModel> Addnote(NoteModel note)
        {
            var result = await _notesService.AddNote(note);
            return result;
        }
        [HttpGet("{Id}")]
        public async Task<NoteModel> GetById(int Id)
        {
            var result = await _notesService.GetNoteById(Id);
            return result;
        }
    }
}
