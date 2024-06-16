using Microsoft.EntityFrameworkCore;
using docker_api.Data;
using docker_api.Entity;
using Microsoft.Extensions.Caching.Memory;
using docker_api.Controllers;


namespace docker_api.NotesRepository
{
    public class NotesRepository : INotesRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMemoryCache _memoryCache;
        private readonly string cacheKeyName = "notesCache";
        private readonly ILogger<NotesController> _logger;
        public NotesRepository(DataContext dataContext, 
            IMemoryCache memory,
            ILogger<NotesController> logger)
        {
            _dataContext = dataContext;
            _memoryCache = memory;
            _logger = logger;

        }
        public async Task<NoteModel> AddNote(NoteModel note)
        {
            try
            {
                var noteModels = await _dataContext.noteModels.AddAsync(note);
                noteModels.Context.SaveChanges();
                return note;
            }
            catch
            {
                if (_memoryCache.TryGetValue(cacheKeyName, out IEnumerable<NoteModel> result))
                {
                    result = result!.Append(note);
                    _memoryCache.Set(cacheKeyName, result);
                }
               
                return note;
            }
           

        }

        public async Task<NoteModel> GetNoteById(int Id)
        {
            try 
            { 
                var _note =  await _dataContext.noteModels.FindAsync(Id);
          
                return _note!;
            }
            catch
            {
                if (_memoryCache.TryGetValue(cacheKeyName, out IEnumerable<NoteModel> result))
                {
                   return result!.FirstOrDefault(e => e.Id == Id)!;
                }
                else
                {
                    return new NoteModel();
                }
            }
            

        }

        public async Task<IEnumerable<NoteModel>> GetAllNotes()
        {
            try
            {
                var noteModels = await _dataContext.noteModels.ToListAsync();
                return noteModels;
            }
            catch {

                
                if(_memoryCache.TryGetValue(cacheKeyName, out IEnumerable<NoteModel> result)){
                    _logger.Log(LogLevel.Information, "Notes found in cache");
                }
                else
                {
                    result = Enumerable.Range(1, 5).Select(index => new NoteModel
                    {
                        CreatedDate = DateTime.Now.AddDays(index),
                        Id = index,
                        Title = $"Note#{index}",
                        Description = $"Description Note #{index}"
                    })
                    .ToList();
                    _logger.Log(LogLevel.Information, "Notes Not found in cache");
                    var cacheEntryOption = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(5000))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(5000))
                        .SetPriority(CacheItemPriority.Normal);
                    _memoryCache.Set(cacheKeyName, result, cacheEntryOption);

                }
                return result!;
                
            }
           
        }

        
    }
}
