using EShopper.Core.Entities;

namespace EShopper.Entities.Models
{
    public partial class ProductCategory : IEntity
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }

        public virtual Product Product { get; set; }
    }
}