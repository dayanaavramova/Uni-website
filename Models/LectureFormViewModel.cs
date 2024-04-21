using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using University.Data.Models;
using static University.Data.Constants.DataConstants;

namespace University.Models
{
    public class LectureFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = StringLenghtErrorMessage)]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int ProfessorId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(LectureDetailsMaxLenght,
            MinimumLength = LectureDetailsMinLenght,
            ErrorMessage = StringLenghtErrorMessage)]
        public string Details { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string DateAndTime { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(LectureDurationMinValue, LectureDurationMaxValue, ErrorMessage = StringLenghtErrorMessage)]
        public int Duration { get; set; }
    }
}
