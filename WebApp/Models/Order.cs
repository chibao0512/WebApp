using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Id { get; set; }
        public string Order_FullName { get; set; }
        public string Order_Email { get; set; }
        public int Order_Phone { get; set; }
        public string Order_Address { get; set; }
        public virtual ICollection<OrderDetail> OrdersDetails { get; set; }
    }
}
