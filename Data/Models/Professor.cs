using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;
using Microsoft.AspNetCore.Identity;

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

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public IEnumerable<Lecture> Lectures { get; set; } = new List<Lecture>();
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
    }
}
