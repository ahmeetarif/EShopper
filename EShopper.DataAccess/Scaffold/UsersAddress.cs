using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class UsersAddress
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public int PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string Addressline2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
