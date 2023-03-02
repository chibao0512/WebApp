using ASM_DEMO_1670.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_DEMO_1670.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUsers { get; set; }
    }
}
