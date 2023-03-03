using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_DEMO_1670.Models
{
    public class CartDetail
    {
        
     
        public int Quantity { get; set; }
        public string Cus_Id { get; set; }
        public  Book? Book { get; set; }

       
    }
}
