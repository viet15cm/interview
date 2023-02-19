using Book.Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book.Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {}

        public DbSet<BookEntity>? Books { get; set; }
    }
}
