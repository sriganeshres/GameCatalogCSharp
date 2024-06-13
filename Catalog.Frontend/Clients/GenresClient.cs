using Catalog.Frontend.Models;

namespace Catalog.Frontend.Clients;

public class GenresClient(HttpClient client)
{
    public async Task<Genre[]> GetGenresAsync()
        => await client.GetFromJsonAsync<Genre[]>("genre") ?? [];
}
