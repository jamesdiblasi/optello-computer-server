namespace ComputerUse.Application
{
    public record BaseImageCommandResponse : BaseCommandResponse
    {
        public required string Base64File { get; init; }
    }
}
