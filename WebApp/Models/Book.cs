using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
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
        public decimal Book_Price { get; set; }
        public string Book_Publisher { get; set; }
        public string Book_Description { get; set; }
        public int Gen_Id { get; set; }
        [ForeignKey("Gen_Id")]
        public virtual Genre ? genre { get; set; }
    }
}
