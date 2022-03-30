using QaryaHealth.Core.Entities;
using QaryaHealth.Core.IRepositories;

namespace QaryaHealth.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(QaryaHealthDbContext qaryaHealthDbContext) 
            : base(qaryaHealthDbContext)
        {
        }
    }
}
