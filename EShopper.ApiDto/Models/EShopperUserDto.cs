namespace EShopper.ApiDto.Models
{
    public class EShopperUserDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public bool EmailConfirmed { get; set; }

        public virtual UserDetailsDto UserDetails { get; set; }
    }
}