using ActionCommandGame.Sdk.Abstractions;
using System.Threading.Tasks;

namespace ActionCommandGame.Ui.ConsoleApp.Stores;

public class TokenStore : ITokenStore
{
    private string Token { get; set; }

    public Task<string> GetTokenAsync()
    {
        return Task.FromResult(Token);
    }

    public Task SaveTokenAsync(string token)
    {
        Token = token;

        return Task.CompletedTask;
    }
}