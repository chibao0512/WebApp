using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }   
        
        public DbSet<Genre> genres {  get; set; }
        public DbSet<Publisher> publishers { get; set; }
        public DbSet<Book> books { get; set; }
    }
}