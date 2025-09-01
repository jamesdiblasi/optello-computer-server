namespace ComputerUse.Core.EnvironmentTools.Keyboard
{
    public record HoldenKey : Key
    {
        public required int Duration { get; init; }
    }
}
