using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Core.EnvironmentTools.MouseAutomation
{
    public record ScrollAction
    {
        public required ScrollDirection ScrollDirection { get; init; }

        public required int ScrollAmount { get; init; }
    }
}
