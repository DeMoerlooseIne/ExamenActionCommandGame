using System.Net.Http.Json;
using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Blazored.LocalStorage;

namespace ActionCommandGame.Sdk
{
    public class NegativeGameEventApi : INegativeGameEventApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorageService;

        public NegativeGameEventApi(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> GetAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"negativeGameEvents/{id}";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<NegativeGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<IList<NegativeGameEventResult>>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = "negativeGameEvents";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<IList<NegativeGameEventResult>>>();

            if (result is null)
            {
                return new ServiceResult<IList<NegativeGameEventResult>>();
            }

            return result;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> Create(NegativeGameEventResult negativeGameEventResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = "negativeGameEvents";

            var httpResponse = await httpClient.PostAsJsonAsync(route, negativeGameEventResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<NegativeGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> Update(int id, NegativeGameEventResult negativeGameEventResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"negativeGameEvents/{id}";

            var httpResponse = await httpClient.PutAsJsonAsync(route, negativeGameEventResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<NegativeGameEventResult>();
            }

            return result;
        }

        public async Task<ServiceResult<NegativeGameEventResult>> DeleteAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _localStorageService.GetItemAsync<string>("Token");

            httpClient.AddAuthorization(token);
            var route = $"negativeGameEvents/{id}";

            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<NegativeGameEventResult>>();

            if (result is null)
            {
                return new ServiceResult<NegativeGameEventResult>();
            }

            return result;
        }
    }
}
