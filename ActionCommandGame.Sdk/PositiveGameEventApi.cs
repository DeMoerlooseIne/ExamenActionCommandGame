using System.Net.Http.Json;
using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Filters;
using ActionCommandGame.Services.Model.Results;
using Blazored.LocalStorage;

namespace ActionCommandGame.Sdk
{
    public class PositiveGameEventApi : IPositiveGameEventApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public PositiveGameEventApi(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> GetAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"positiveGameEvents/{id}";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<PositiveGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<IList<PositiveGameEventResult>>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = "positiveGameEvents";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<IList<PositiveGameEventResult>>>();

            if (result is null)
            {
                return new ServiceResult<IList<PositiveGameEventResult>>();
            }

            return result;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> Create(PositiveGameEventResult positiveGameEventResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = "positiveGameEvents";

            var httpResponse = await httpClient.PostAsJsonAsync(route, positiveGameEventResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<PositiveGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> Update(int id, PositiveGameEventResult positiveGameEventResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"positiveGameEvents/{id}";

            var httpResponse = await httpClient.PutAsJsonAsync(route, positiveGameEventResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<PositiveGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<PositiveGameEventResult>> DeleteAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"positiveGameEvents/{id}";

            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PositiveGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<PositiveGameEventResult>();
            }

            return result;
        }
    }
}
