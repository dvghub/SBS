using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL {
    public class MoveContext : DbContext{
        public DbSet<Move> Moves { get; set; } 
        public DbSet<MoveRating> Ratings { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Integrated Security=SSPI;AttachDBFilename=C:\\Users\\moili\\MoveContext.mdf");
            base.OnConfiguring(builder);
        }
    }
}
