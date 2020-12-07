using System;
using System.Collections.Generic;

namespace EShopper.Dto.Models
{
    public class SellerUsersDto
    {
        public SellerUsersDto()
        {
            ProductDto = new HashSet<ProductDto>();
        }

        public string UserId { get; set; }
        public string StoreName { get; set; }
        public long Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EShopperUserDto UserDto { get; set; }
        public virtual ICollection<ProductDto> ProductDto { get; set; }
    }
}