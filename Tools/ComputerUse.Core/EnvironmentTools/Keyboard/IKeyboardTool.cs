namespace ComputerUse.Core.EnvironmentTools.Keyboard
{
    public interface IKeyboardTool
    {
        public Task PressKeyAsync(Key key);

        public Task HoldKeyAsync(HoldenKey key);

        public Task TypeTextAsync(TypedText typedText);

        public Task KeyDownAsync(Key key);

        public Task KeyUpAsync(Key key);
    }
}
