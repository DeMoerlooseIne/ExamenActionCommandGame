using System.Dynamic;
using ActionCommandGame.Sdk.Abstractions;
using Blazored.LocalStorage;

namespace ActionCommandGame.BlazorApp.Stores
{
    public class TokenStore: ITokenStore
    {
        private const string TokenName = "JwtToken";
        private readonly ILocalStorageService _localStorage;

        public TokenStore(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("Token");
        }

        public async Task SaveTokenAsync(string token)
        {
            await _localStorage.SetItemAsStringAsync(TokenName, token);
        }
    }
}
