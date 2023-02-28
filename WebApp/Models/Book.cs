using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Book_Id { get; set; }
        [Required]
        public string Book_Title { get; set; }
        [NotMapped]
        public IFormFile? Book_Image { get; set; }
        public string Book_Author { get; set; }
        public int Genre_Id { get; set; }
        [ForeignKey("Genre_Id")]
        public decimal Book_Price { get; set; }
        public string Book_Description { get; set; }
        public virtual Genre?genre { get; set; }
    }
}
