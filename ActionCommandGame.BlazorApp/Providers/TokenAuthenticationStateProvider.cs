using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ActionCommandGame.Sdk.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;

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

        // Get the ClaimsPrincipal object containing the roles
        //var principal = GetJwtToken(token);
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

    //private ClaimsPrincipal GetJwtToken(string token)
    //{
    private JwtSecurityToken GetJwtToken(string token)
    {
        //var handler = new JwtSecurityTokenHandler();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();
        // Extract the roles from the claims
        var roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        // Add the roles to the claims identity
        var claimsIdentity = new ClaimsIdentity(claims, "jwt");
        foreach (var role in roles) claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        // Create a ClaimsPrincipal object with the claims identity
        //return new ClaimsPrincipal(claimsIdentity);
        return handler.ReadJwtToken(token);
    }
}