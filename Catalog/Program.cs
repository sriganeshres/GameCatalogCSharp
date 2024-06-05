using Catalog.Data;
using Catalog.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("CatalogStore");
builder.Services.AddSqlite<CatalogContext>(connString);

var app = builder.Build();


app.GetGameEndPoints();
await app.MigrateDBAsync();

app.Run();
