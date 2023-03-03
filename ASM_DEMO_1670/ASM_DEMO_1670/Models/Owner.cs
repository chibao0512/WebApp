using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASM_DEMO_1670.Models
{
    public class Owner
    {

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage = "Phone number must have 10 digits", MinimumLength = 10)]
        public string Phone { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
