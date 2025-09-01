namespace ComputerUse.Agent.Contracts
{
    public record Base64ImageContentContract : IContentContract
    {
        public required string MediaType { get; init; }

        /// <summary>
        /// Base64 image data
        /// </summary>
        public required string Data { get; init; }
    }
}
