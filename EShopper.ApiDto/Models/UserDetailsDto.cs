using System;

namespace EShopper.ApiDto.Models
{
    public class UserDetailsDto
    {
        public string Fullname { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}