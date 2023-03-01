using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class ChangePassword
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
