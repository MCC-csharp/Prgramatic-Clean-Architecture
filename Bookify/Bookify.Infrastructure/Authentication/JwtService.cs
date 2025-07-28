using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;
internal sealed class JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions) : IJwtService
{
    private static readonly Error AuthenticationFailed = new(
    "Keycloak.AuthenticationFailed",
    "Failed to acquire access token do to authentication failure");

    private readonly HttpClient _httpClient = httpClient;
    private readonly IOptions<KeycloakOptions> _keycloakOptions = keycloakOptions;

    public async Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            using var authorizationRequestContent = new FormUrlEncodedContent(
            [
                new("client_id", _keycloakOptions.Value.AuthClientId),
                new("client_secret", _keycloakOptions.Value.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", email),
                new("password", password)
            ]);

            HttpResponseMessage response = await _httpClient.PostAsync("", authorizationRequestContent, cancellationToken);
            response.EnsureSuccessStatusCode();
            AuthorizationToken? authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            if (authorizationToken is null)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return authorizationToken.AccessToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}
