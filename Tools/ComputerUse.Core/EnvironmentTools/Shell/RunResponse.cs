using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Core.EnvironmentTools.Shell
{
    public record RunResponse
    {
        public required string StandardOutput { get; set; }
        public required int ExitCode { get; set; }
    }
}
