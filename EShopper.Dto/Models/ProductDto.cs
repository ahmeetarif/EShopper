using System;
namespace EShopper.Dto.Models
{
    public class ProductDto
    {
        public string SellerId { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public short Quanity { get; set; }
        public int Score { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual CategoryDto CategoryDto { get; set; }
        public virtual SellerUsersDto SellerDto { get; set; }
        public virtual SubCategoryDto SubCategoryDto { get; set; }
    }
}
