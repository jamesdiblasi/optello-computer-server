using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure.Azure
{
    internal class AzureHttpToolsJwtAuthService : IHttpToolsJwtAuthService
    {
        private const string AZURE_TOKEN_SERVICE_URL = "https://dynamicsessions.io/.default";
        private const string AZURE_TOKEN_CACHE_KEY = "AZURE_TOKEN_CACHE_KEY";

        private readonly ILogger logger;
        private readonly IMemoryCache memoryCache;
        private readonly bool defaultAzureCredentialIncludeInteractiveCredentials;


        public AzureHttpToolsJwtAuthService(ILogger<HttpToolsJwtAuthHandler> logger, IMemoryCache memoryCache, IOptions<ComputerUseInfrastructureOptions> optionsSlice) 
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.defaultAzureCredentialIncludeInteractiveCredentials = optionsSlice.Value.ToolsClient.DefaultAzureCredentialIncludeInteractiveCredentials;
        }

        public async Task<string> GetJwtTokenAsync()
        {
            try
            {
                if (!memoryCache.TryGetValue(AZURE_TOKEN_CACHE_KEY, out AccessToken token))
                {
                    var credential = new DefaultAzureCredential(this.defaultAzureCredentialIncludeInteractiveCredentials);
                    token = await credential.GetTokenAsync(new TokenRequestContext(new[] { AZURE_TOKEN_SERVICE_URL }));

                    var refreshOn = token.RefreshOn ?? token.ExpiresOn.AddMinutes(-5);

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(refreshOn);

                    memoryCache.Set(AZURE_TOKEN_CACHE_KEY, token, cacheEntryOptions);
                }

                return token.Token;
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex, $"Exception occurs during retrieving azure token from {AZURE_TOKEN_SERVICE_URL}");

                throw;
            }
        }
    }
}
