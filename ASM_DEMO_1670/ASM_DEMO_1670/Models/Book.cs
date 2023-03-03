using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_DEMO_1670.Models
{
    public class Book
    {
     
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Book_Id { get; set; }
            [Required]
            public string Book_Title { get; set; }
            [Required]
            public string Book_Author { get; set; }
            [Required]
            public double Book_Price { get; set; }
            public string Book_Publisher { get; set; }
            public string Book_Description { get; set; }
            [NotMapped]
            public IFormFile? Image { get; set; }
            public string? urlImage { get; set; }
            public int genre_Id { get; set; }
            [ForeignKey("genre_Id")]
            public virtual Genre? genre { get; set; }
            public virtual ICollection<OrderDetail>? OrderDetails { get; set; }


        }
    }
