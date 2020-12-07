using System.Collections.Generic;

namespace EShopper.Dto.Models
{
    public class SubCategoryDto
    {
        public SubCategoryDto()
        {
            ProductDto = new HashSet<ProductDto>();
        }

        public long ParentCategoryId { get; set; }
        public string Title { get; set; }

        public virtual CategoryDto ParentCategoryDto { get; set; }
        public virtual ICollection<ProductDto> ProductDto { get; set; }
    }
}