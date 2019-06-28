using Contracts;
using DAL;
using Models;

namespace Repos
{
   public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(MyContext context, IUnitOfWork unitOfWork)
            :base(context, unitOfWork)
        {}

    }
}
