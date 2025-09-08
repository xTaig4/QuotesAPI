using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuoteAPI.Entities;
using QuoteAPI.Models;

namespace QuoteAPI.Data
{
    public class QuoteContext : DbContext
    {
       public DbSet<Quote> Quotes { get; set; }
       public DbSet<User> Users { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbPath = System.IO.Path.Combine(AppContext.BaseDirectory, "app.db");
            options.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>().Property(c => c.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            modelBuilder.Entity<Quote>().HasData(
                new Quote() { Id = 2, 
                            firstName = "Itachi", 
                            lastName="Uchiha", 
                            quote= "Each of us lives, dependent and bound by our individual knowledge and our awareness. All that is what we call reality. However, both knowledge and awareness are equivocal. One’s reality might be another’s illusion. We all live inside our own fantasies.", 
                            image= "https://i.pinimg.com/736x/72/b7/18/72b718edd0ad5bb48601e2243c4663b4.jpg",
                            anime= "Naruto"
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User() {
                    Id = 1,
                    Username = "guest", 
                    PasswordHash = "guest" }
            );
        }
    }
}
