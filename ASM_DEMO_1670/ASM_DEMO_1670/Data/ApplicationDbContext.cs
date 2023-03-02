using ASM_DEMO_1670.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASM_DEMO_1670.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(m => new { m.OrderId, m.BookId });
            base.OnModelCreating(modelBuilder);

        }
    }
}