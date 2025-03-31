using Account.Models;
using Microsoft.EntityFrameworkCore;

namespace Account.Data
{
    public class UserDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public UserDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("UserServiceConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Database relations
            
            modelBuilder.Entity<Player>() // Player-Friends relations
            .HasMany(p => p.Friends)
            .WithMany()
            .UsingEntity(j => j.ToTable("PlayerFriends"));
            
        }
        public DbSet<Player> Players { get; set; }
    }
}