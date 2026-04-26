using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Entities.Infrastructure.Http
{
    public class HttpClientAuthorizationDelegatingHandler
        : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccesor.HttpContext is { } httpContext)
            {
                var authorizationHeader = httpContext.Request.Headers["Authorization"];

                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
                }

                var token = await GetToken(httpContext);

                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private static async Task<string?> GetToken(HttpContext httpContext)
        {
            const string ACCESS_TOKEN = "access_token";
            return await httpContext.GetTokenAsync(ACCESS_TOKEN);
        }
    }
}
