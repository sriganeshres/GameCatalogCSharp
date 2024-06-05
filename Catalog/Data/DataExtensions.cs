using Microsoft.EntityFrameworkCore;

namespace Catalog.Data;

public static class DataExtensions
{
    public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
        dbContext.Database.Migrate();
    }
}