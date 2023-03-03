using ASM_DEMO_1670.Models;
using Microsoft.AspNetCore.Identity;

namespace ASM_DEMO_1670.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Acc_Name { get; set; }
        public string? Acc_Image { get; set; }
       
    }
}
