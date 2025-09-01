using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Core
{
    public record ComputerUseExecutionContext
    {
        public required string Action { get; set; }
    }
}
