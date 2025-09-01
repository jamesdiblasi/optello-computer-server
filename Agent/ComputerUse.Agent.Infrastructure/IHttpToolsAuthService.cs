namespace ComputerUse.Agent.Infrastructure
{
    internal interface IHttpToolsJwtAuthService
    {
        public Task<string> GetJwtTokenAsync();
    }
}
