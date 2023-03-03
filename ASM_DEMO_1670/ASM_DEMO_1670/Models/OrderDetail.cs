using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_DEMO_1670.Models
{
    public class OrderDetail
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        public int OrderId { get; set; }
      
        public int BookId { get; set; }
       
        public int Quantity { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public virtual Order? order { get; set; }
        [ForeignKey("BookId")]
        [JsonIgnore]
        public virtual Book?Book { get; set; }
    }
}
