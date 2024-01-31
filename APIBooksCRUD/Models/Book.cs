using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBooksCRUD.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        public int GenreId { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public DateOnly PublicationDate { get; set; }

        public int PageCount { get; set; }

        [Column(TypeName = "text")]
        public string Synopsis { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
