using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int Order_Id { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Book_id { get; set; }
        [ForeignKey("Order_id")]
        public virtual Order order { get; set; }
        [ForeignKey("Book_Id")]
        public virtual Book Book { get; set; }
    }
}
