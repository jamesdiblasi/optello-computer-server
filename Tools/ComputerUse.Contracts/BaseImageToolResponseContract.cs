namespace ComputerUse.Contracts
{
    public record BaseImageToolResponseContract : BaseToolResponseContract
    {
        public required string Base64File { get; init; }
    }
}
