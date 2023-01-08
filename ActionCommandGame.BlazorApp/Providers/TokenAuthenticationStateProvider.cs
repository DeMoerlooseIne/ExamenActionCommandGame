using ActionCommandGame.Sdk.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ActionCommandGame.BlazorApp.Providers;

public class TokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStore _tokenStore;

    public TokenAuthenticationStateProvider(ITokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStore.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token)) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var jwtToken = GetJwtToken(token);

        var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    public void NotifyAuthenticationStateChanged()
    {
        var authenticateState = GetAuthenticationStateAsync();

        NotifyAuthenticationStateChanged(authenticateState);
    }

    private JwtSecurityToken GetJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(token);
    }
}