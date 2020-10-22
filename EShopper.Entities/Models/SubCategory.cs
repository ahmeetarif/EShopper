using EShopper.Core.Entities;
using System.Collections.Generic;

namespace EShopper.Entities.Models
{
    public partial class SubCategory : IEntity
    {
        public SubCategory()
        {
            Product = new HashSet<Product>();
        }

        public long Id { get; set; }
        public long ParentCategoryId { get; set; }
        public string Title { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}