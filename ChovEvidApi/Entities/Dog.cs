using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChovEvid.Entities
{
    public class Dog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int IdOwner { get; set; }

        [Required]
        public required string IdBreedingStation { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(1)]
        public required string Sex { get; set; }

        [MaxLength(50)]
        public required string State { get; set; }
    }
}
