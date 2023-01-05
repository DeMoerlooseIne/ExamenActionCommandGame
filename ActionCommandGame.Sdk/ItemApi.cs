using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace ActionCommandGame.Sdk
{
    public class ItemApi: IItemApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenStore _tokenStore;


        public ItemApi(IHttpClientFactory httpClientFactory, ITokenStore tokenStore)
        {
            _httpClientFactory = httpClientFactory;
            _tokenStore = tokenStore;
        }

        public async Task<ServiceResult<ItemResult>> GetAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _tokenStore.GetTokenAsync();

            httpClient.AddAuthorization(token);
            var route = $"items/{id}";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();

            if (result is null)
            {
                return new ServiceResult<ItemResult>();
            }

            return result;
        }

        public async Task<ServiceResult<IList<ItemResult>>> FindAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _tokenStore.GetTokenAsync();
            httpClient.AddAuthorization(token);
            var route = "items";

            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<IList<ItemResult>>>();

            if (result is null)
            {
                return new ServiceResult<IList<ItemResult>>();
            }

            return result;
        }

        public async Task<ServiceResult<ItemResult>> Create(ItemResult itemResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _tokenStore.GetTokenAsync();

            httpClient.AddAuthorization(token);
            var route = "items";

            var httpResponse = await httpClient.PostAsJsonAsync(route, itemResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();

            if (result is null)
            {
                return new ServiceResult<ItemResult>();
            }

            return result;
        }

        public async Task<ServiceResult<ItemResult>> Update(int id, ItemResult itemResult)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _tokenStore.GetTokenAsync();

            httpClient.AddAuthorization(token);
            var route = $"items/{id}";

            var httpResponse = await httpClient.PutAsJsonAsync(route, itemResult);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();

            if (result is null)
            {
                return new ServiceResult<ItemResult>();
            }

            return result;
        }

        public async Task<ServiceResult<ItemResult>> DeleteAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("ActionCommandGame");
            var token = await _tokenStore.GetTokenAsync();

            httpClient.AddAuthorization(token);
            var route = $"items/{id}";

            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var result = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<ItemResult>>();

            if (result is null)
            {
                return new ServiceResult<ItemResult>();
            }

            return result;
        }
    }
}
