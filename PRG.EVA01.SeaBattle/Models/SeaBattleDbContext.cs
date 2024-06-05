using Microsoft.EntityFrameworkCore;
using PRG.EVA01.anthony.mohamed1.Models;

namespace PRG.EVA01.SeaBattle.Models
{
    public class SeaBattleDbContext : DbContext
    {
        public DbSet<GameLog> GameLogs { get; set; }
        public DbSet<Game> game { get; set; }
        public SeaBattleDbContext(DbContextOptions<SeaBattleDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configureren van de relatie tussen Game en GameLog
            modelBuilder.Entity<Game>()
                .HasMany(g => g.GameLogs)
                .WithOne(gl => gl.Game)
                .HasForeignKey(gl => gl.GameId)
                .OnDelete(DeleteBehavior.Cascade);  // Hiermee wordt de juiste relatie geconfigureerd

            // Configureren van de relatie tussen Game en Boat
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Boats)
                .WithOne(b => b.Game)
                .HasForeignKey(b => b.GameId)
                .OnDelete(DeleteBehavior.Cascade);  // Hiermee wordt de juiste relatie geconfigureerd

            base.OnModelCreating(modelBuilder);
        }
    }
}