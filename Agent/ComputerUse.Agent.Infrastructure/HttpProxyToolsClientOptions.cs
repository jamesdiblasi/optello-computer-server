namespace ComputerUse.Agent.Infrastructure
{
    internal class HttpProxyToolsClientOptions
    {
        public required string BaseUrl { get; init; }

        public required bool EnableJwtTokenAuth { get; init; } = true;

        public bool DefaultAzureCredentialIncludeInteractiveCredentials { get; init; } = false;
    }
}
