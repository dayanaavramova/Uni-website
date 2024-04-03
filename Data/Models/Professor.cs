using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using static University.Data.DataConstants;

namespace University.Data.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(NameMaxLenght)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Range(ProfRatingMinValue, ProfRatingMaxValue)]
        public double Rating { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;

        public IEnumerable<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}
