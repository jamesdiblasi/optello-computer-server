using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Core.Tools
{
    public record BaseImageToolResponse : BaseToolResponse
    {
        public required string Base64File { get; init; }
    }
}
