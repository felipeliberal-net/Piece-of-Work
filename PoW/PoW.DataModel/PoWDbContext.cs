using PoW.DataModel.Models;
using System.Data.Entity;

namespace PoW.DataModel
{
    public class PoWDbContext : DbContext
    {
        public PoWDbContext() : base("PoWConnectionString")
        {
        }

        public static PoWDbContext Create()
        {
            return new PoWDbContext();
        }

        public DbSet<TaskWork> TaskWorks { get; set; }
    }
}
