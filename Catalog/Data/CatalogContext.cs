using Catalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data;

public class CatalogContext(
    DbContextOptions<CatalogContext> options
    ) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { ID = 1, Name = "Fighting" },
            new { ID = 2, Name = "Strategy" },
            new { ID = 3, Name = "SandBox" },
            new { ID = 4, Name = "Sports" }
        );
    }
}
