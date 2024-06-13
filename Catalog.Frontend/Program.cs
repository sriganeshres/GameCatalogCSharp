using Catalog.Frontend.Clients;
using Catalog.Frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var catalogApiURl = builder.Configuration["CatalogApiURL"] ?? throw new Exception("Backend URL is not set");

builder.Services.AddHttpClient<GamesClient>(client =>
{
    client.BaseAddress = new Uri(catalogApiURl);
});

builder.Services.AddHttpClient<GenresClient>(client =>
{
    client.BaseAddress = new Uri(catalogApiURl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
