using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static University.Data.DataConstants;

namespace University.Data.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        public int ProfessorId { get; set; }

        [Required]
        [ForeignKey(nameof(ProfessorId))]
        public Professor Professor { get; set; } = null!;

        [Required]
        [MaxLength(LectureDetailsMaxLenght)]
        public string Details { get; set; } = string.Empty;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        [Range(LectureDurationMinValue, LectureDurationMaxValue)]
        public int Duration { get; set; }

        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
