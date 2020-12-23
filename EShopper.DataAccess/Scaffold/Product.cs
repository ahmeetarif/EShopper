﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class Product
    {
        public long Id { get; set; }
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

        public virtual Category Category { get; set; }
        public virtual SellerUser Seller { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
