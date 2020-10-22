using EShopper.Core.Entities;
using System.Collections.Generic;

namespace EShopper.Entities.Models
{
    public partial class Category : IEntity
    {
        public Category()
        {
            Product = new HashSet<Product>();
            SubCategory = new HashSet<SubCategory>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<SubCategory> SubCategory { get; set; }
    }
}