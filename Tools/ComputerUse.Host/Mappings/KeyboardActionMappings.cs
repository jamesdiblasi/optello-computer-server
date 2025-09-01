using ComputerUse.Application.PressKey;
using ComputerUse.Application.TypeText;
using ComputerUse.Contracts.PressKey;
using ComputerUse.Contracts.TypeText;

namespace ComputerUse.Host.Mappings
{
    public static class KeyboardActionMappings
    {
        public static PressKeyCommand ToCommand(this PressKeyRequest contract)
        {
            return new PressKeyCommand { Text = contract.Text, Duration = contract.Duration };
        }

        public static TypeTextCommand ToCommand(this TypeTextRequest contract)
        {
            return new TypeTextCommand { Text = contract.Text };
        }
    }
}
