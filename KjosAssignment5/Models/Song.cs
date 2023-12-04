using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KjosAssignment5.Models
{
    public class Song
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string? Genre { get; set; }

        [StringLength(30, MinimumLength = 1)]
        [Required]
        public string? Artist { get; set; }

        [Range(1,100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
