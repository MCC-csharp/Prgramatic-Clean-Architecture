﻿using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;


namespace Bookify.Infrastructure.Authentication;

public sealed class AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloackOptions) : DelegatingHandler
{
    private readonly KeycloakOptions _keycloackOptions = keycloackOptions.Value;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AuthorizationToken authorizationToken = await GetAuthorizationToken(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, authorizationToken.AccessToken);

        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);

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
        using var authorizationRequest = new HttpRequestMessage(HttpMethod.Post,
             new Uri(_keycloackOptions.TokenUrl))
        {
            Content = authorizationRequestContent
        };
        HttpResponseMessage authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken)
               ?? throw new ApplicationException();
    }
}
