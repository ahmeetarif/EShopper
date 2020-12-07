using EShopper.DataAccess.Repository.Abstract;
using EShopper.Entities.Models;

namespace EShopper.DataAccess.Repository.Concrete
{
    public class SubCategoryRepository : GenericRepository<SubCategory, long>, ISubCategoryRepository
    {
        public SubCategoryRepository(EShopperDbContext context)
            : base(context)
        {
        }
    }
}