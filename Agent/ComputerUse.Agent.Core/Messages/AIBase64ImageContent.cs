namespace ComputerUse.Agent.Core.Messages
{
    public record AIBase64ImageContent : IAIContent
    {
        public required string MediaType { get; init; }

        /// <summary>
        /// Base64 image data
        /// </summary>
        public required string Data { get; init; }
    }
}
