using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace Bookify.Infrastructure.Authentication
{
    public sealed class AdminAuthorizationDelegatingHandler(KeycloakOptions keycloackOptions) : DelegatingHandler
    {
        private readonly KeycloakOptions _keycloackOptions = keycloackOptions;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationToken = await GetAuthorizationToken(cancellationToken);

            request.Headers.Authorization =  new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, authorizationToken.AccessToken);

            var httpResponseMessage = await base.SendAsync(request, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }

        private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
        {
            var authorizationRequestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _keycloackOptions.AdminClientId),
                new("client_secret", _keycloackOptions.AdminClientSecret),
                new("scope", "openid email"),
                new("grant_type", "client_credentials")
            };

            var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);
            var authorizationRequest = new HttpRequestMessage(HttpMethod.Post,
                new Uri(_keycloackOptions.TokenUrl))
            {
                Content = authorizationRequestContent
            };
            var authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

            authorizationResponse.EnsureSuccessStatusCode();

            return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>()
                   ?? throw new ApplicationException();
        }
    }
}
