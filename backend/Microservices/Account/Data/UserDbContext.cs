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
            // For data base relations
            modelBuilder.Entity<Player>() // Player-Tournaments relations
            .HasMany(p => p.Tournaments)
            .WithMany(t => t.Players);

            modelBuilder.Entity<Player>() // Player-Friends relations
            .HasMany(p => p.Friends)
            .WithMany()
            .UsingEntity(j => j.ToTable("PlayerFriends"));

            modelBuilder.Entity<Tournament>() // Tournament-Players relations
            .HasMany(t => t.Players);

            modelBuilder.Entity<Tournament>() // Tournament-Winner relations
            .HasOne(t => t.Winner);
            
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
    }
}