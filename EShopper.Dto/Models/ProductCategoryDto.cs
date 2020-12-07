namespace EShopper.Dto.Models
{
    public class ProductCategoryDto
    {
        public long ProductId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }

        public virtual ProductDto ProductDto { get; set; }
    }
}