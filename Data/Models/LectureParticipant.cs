using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Data.Models
{
    public class LectureParticipant
    {
        [Required]
        public int LectureId { get; set; }
        [Required]
        [ForeignKey(nameof(LectureId))]
        public Lecture Lecture { get; set; } = null!;

        [Required]
        public int StudentId { get; set; }
        [Required]
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; } = null!;
    }
}
