using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class CategoryRepository : GenericRepository<Category, long>, ICategoryRepository
    {
        public CategoryRepository(EShopperDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
