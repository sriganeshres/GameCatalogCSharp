using Catalog.Frontend.Models;

namespace Catalog.Frontend.Clients;

public class GamesClient(HttpClient client)
{
    public async Task<GameDetails[]> GetGamesAsync()
        => await client.GetFromJsonAsync<GameDetails[]>("games") ?? [];

    public async Task AddGameAsync(GameClientDetails game)
        => await client.PostAsJsonAsync("games", game);


    public async Task<GameClientDetails> GetDetailsAsync(int id)
        => await client.GetFromJsonAsync<GameClientDetails>($"games/{id}") ?? throw new Exception("Could not Find Game");

    public async Task UpdateGameAsync(GameClientDetails gameUpdt)
        => await client.PutAsJsonAsync($"games/{gameUpdt.ID}", gameUpdt);

    public async Task DeleteGameAsync(int id)
        => await client.DeleteAsync($"games/{id}");

}
