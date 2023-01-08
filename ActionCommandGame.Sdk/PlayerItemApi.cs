using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace ActionCommandGame.Sdk;

public class PlayerItemApi : IPlayerItemApi
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILocalStorageService _localStorageService;

    public PlayerItemApi(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _localStorageService = localStorageService;
    }

    public async Task<ServiceResult<IList<PlayerItemResult>>> FindAsync(PlayerItemFilter filter)
    {
        var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
        var token = await _localStorageService.GetItemAsync<string>("Token");

        httpClient.AddAuthorization(token);
        var route = "player-items";

        if (filter.PlayerId.HasValue) route += $"?playerId={filter.PlayerId}";

        var httpResponse = await httpClient.GetAsync(route);

        httpResponse.EnsureSuccessStatusCode();

        var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<IList<PlayerItemResult>>>();

        if (result is null) return new ServiceResult<IList<PlayerItemResult>>();

        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
        var token = await _localStorageService.GetItemAsync<string>("Token");

        httpClient.AddAuthorization(token);
        var route = $"player-items/{id}";

        var httpResponse = await httpClient.DeleteAsync(route);

        httpResponse.EnsureSuccessStatusCode();
    }
}