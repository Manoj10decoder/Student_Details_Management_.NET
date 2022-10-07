using StudentsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentsManagement.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Students> Students { get; set; }
        public DbSet<StudentsPersonalDetails> StudentsPersonalDetails { get; set; }
        public DbSet<StudentsAddress> StudentsAddress { get; set; }
    }
}
