using Microsoft.EntityFrameworkCore;
using docker_api.Entity;

namespace docker_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<NoteModel> noteModels { get; set; }
    }
}
