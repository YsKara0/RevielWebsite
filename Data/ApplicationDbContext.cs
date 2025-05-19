using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reviel.Models;

namespace reviel.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }        public DbSet<Product> Products { get; set; }
        public DbSet<Research> Researches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ProductType enum'ını string olarak kaydetmek için
            modelBuilder.Entity<Product>()
                .Property(p => p.Type)
                .HasConversion<string>();
        }
    }
}
