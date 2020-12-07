using System.Collections.Generic;

namespace EShopper.Dto.Models
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            ProductDto = new HashSet<ProductDto>();
            SubCategoryDto = new HashSet<SubCategoryDto>();
        }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductDto> ProductDto { get; set; }
        public virtual ICollection<SubCategoryDto> SubCategoryDto { get; set; }
    }
}