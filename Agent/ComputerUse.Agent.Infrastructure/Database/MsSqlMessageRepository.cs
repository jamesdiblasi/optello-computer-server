using ComputerUse.Agent.Core;
using ComputerUse.Agent.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure.Database
{
    internal class MsSqlMessageRepository : IMessageRepository
    {
        private readonly AppDbContext dbContext;

        public MsSqlMessageRepository(AppDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }

        public Task AddMessageAsync(AIMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AIMessage>> GelAllMessages(string identifier)
        {
            throw new NotImplementedException();
        }
    }
}
