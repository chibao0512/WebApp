using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Cart
    {
       
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cart_Id { get; set; }
        public int Cart_Quantity { set; get; }
        public Book book { set; get; }
    }
}
