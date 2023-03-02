using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASM_DEMO_1670.Models
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int genre_Id { get; set; }
        [Required]
       
        public string genre_Name { get; set; }
        [Required]
        
        public string genre_Description { get; set; }
        public string? genre_Status { get; set; }

        public virtual ICollection<Book>? Books { get; set; }
    }
}
