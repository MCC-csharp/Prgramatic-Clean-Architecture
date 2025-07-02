using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class AuthenticationService(HttpClient httpClient) : IAuthenticationService
    {
        private readonly HttpClient _httpClient = httpClient;
        private const string PasswordCredentialType = "password";


        public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials = new CredentialRepresentationModel[]
            {
                new()
                {
                    Value =password,
                    Temporary = false,
                    Type = PasswordCredentialType
                }
            };

            var response = await _httpClient.PostAsJsonAsync("users", userRepresentationModel, cancellationToken);

            return ExtractIdentityFromLocationHeader(response);
        }

        private static string ExtractIdentityFromLocationHeader(HttpResponseMessage httpResponseMessage)
        {
            const string usersSegmentName = "users/";

            var locationHeader = (httpResponseMessage.Headers.Location?.PathAndQuery) ?? throw new InvalidOperationException("Location header can't be null");

            var userSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.InvariantCultureIgnoreCase);

            var userIdentityId = locationHeader[(userSegmentValueIndex + usersSegmentName.Length)..];

            return userIdentityId;
        }
    }
}
