using Contracts;
using DAL;
using Models;

namespace Repos
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(MyContext context, IUnitOfWork unitOfWork)
            :base(context,unitOfWork)
        {}

    }
}
