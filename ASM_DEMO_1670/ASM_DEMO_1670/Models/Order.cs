using ASM_DEMO_1670.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASM_DEMO_1670.Models
{
    public class Order
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool status { get; set; }
        public double totalPrice { get; set; }
        public string Cus_ID { get; set; }
        [Required, Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; } 
        public List<OrderDetail> OrderDetails { get; set; }

        
    }
}