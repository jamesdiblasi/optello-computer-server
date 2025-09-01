using ComputerUse.Agent.Core.Messages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerUse.Agent.Infrastructure.Database
{
    internal class AppDbContext : DbContext
    {
        public DbSet<AIMessage> Messages { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
