using ComputerUse.Application;
using ComputerUse.Contracts;

namespace ComputerUse.Host.Mappings
{
    public static class BaseActionMappings
    {
        public static BaseToolResponseContract ToContract(this BaseCommandResponse command)
        {
            return new BaseToolResponseContract { Output = command.Output, Error = command.Error };
        }

        public static BaseImageToolResponseContract ToContract(this BaseImageCommandResponse command)
        {
            return new BaseImageToolResponseContract { Base64File = command.Base64File, Output = command.Output, Error = command.Error };
        }
    }
}
