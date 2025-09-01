using ComputerUse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Infrastructure.Anthropic
{
    internal class ComputerActionTypesFactory : IComputerActionTypesFactory
    {
        public ComputerActionTypes CreateActionType(string actionType) =>
            actionType switch
            {
                "screenshot" => ComputerActionTypes.Screenshot,
                "cursor_position" => ComputerActionTypes.CursorPosition,
                "left_click" => ComputerActionTypes.MouseLeftClick,
                "right_click" => ComputerActionTypes.MouseRightClick,
                "middle_click" => ComputerActionTypes.MouseMiddleClick,
                _ => throw new NotSupportedException($"Action type \'{actionType}\' is not supported")
            };
        
    }
}
