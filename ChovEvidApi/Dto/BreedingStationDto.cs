using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChovEvidApi.Dto
{
    public class BreedingStationDto
    {
        public int RegNumber { get; set; }
        public required string Name { get; set; }
        public required string Owner { get; set; }
        public int DogCount { get; set; }
        public DateTime? Created { get; set; }
        public required string Location { get; set; }
    }
}
