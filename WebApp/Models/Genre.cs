using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Genre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Genre_Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Genre_Name { get; set; }
        [Required]
        public string Genre_Description { get; set; }
         public virtual ICollection<Book> ? Books { get; set; }
    }
}
