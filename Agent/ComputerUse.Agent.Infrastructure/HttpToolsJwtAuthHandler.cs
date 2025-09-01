using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ComputerUse.Agent.Infrastructure
{
    internal class HttpToolsJwtAuthHandler : DelegatingHandler
    {
        private readonly bool isEnabled;
        private readonly IHttpToolsJwtAuthService authService;

        public HttpToolsJwtAuthHandler(IHttpToolsJwtAuthService authService, IOptions<ComputerUseInfrastructureOptions> optionsSlice) 
        {
            this.isEnabled = optionsSlice.Value.ToolsClient.EnableJwtTokenAuth;
            this.authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (isEnabled)
            {
                var jwtAccessToken = await this.authService.GetJwtTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtAccessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.SendAsync(request, cancellationToken).Result;
        }
    }
}
