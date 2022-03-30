using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QaryaHealth.Infrastructure
{
    public class QaryaHealthDbContext: DbContext
    {
        public QaryaHealthDbContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
