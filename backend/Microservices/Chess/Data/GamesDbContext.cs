using Chess.GeneralModels;
using Microsoft.EntityFrameworkCore;

namespace Chess.Data
{
    public class GamesDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public GamesDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ChessServiceConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameInfo>()
                .HasMany(g => g.Fens)
                .WithOne(f => f.GameInfo)
                .HasForeignKey(f => f.GameInfoId)
                .HasPrincipalKey(g => g.Id);
        }

        public DbSet<GameInfo> Games { get; set; }
        public DbSet<FenEntry> Fens { get; set; }
    }
}