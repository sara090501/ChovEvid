using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChovEvidApi.Dto
{
    public class DogDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required string Owner { get; set; }
        public required string Sex { get; set; }
        public required string State { get; set; }
    }
}
