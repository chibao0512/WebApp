using Microsoft.AspNetCore.Identity;

namespace WebApp.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? Acc_Name { get; set; }
        public string? Acc_Image { get; set; }
        public virtual ICollection<Cart>? carts { get; set; }
        public virtual ICollection<Order>? orders { get; set; }
    }
}
