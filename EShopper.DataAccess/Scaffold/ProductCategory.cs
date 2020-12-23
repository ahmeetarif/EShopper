using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class ProductCategory
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }

        public virtual Product Product { get; set; }
    }
}
