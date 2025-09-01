using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure.Database.Entities
{
    internal class DbMessage
    {
        public int? Id { get; init; }

        public required string Identifier { get; init; }
    }
}
