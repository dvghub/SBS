using System;
using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL {
    public class MoveContext : DbContext{
        public DbSet<Move> Moves { get; } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=born2move Trusted_Connection=True;");
            base.OnConfiguring(builder);
        }

    }
}
