using System.ComponentModel.DataAnnotations;
using static University.Data.DataConstants;

namespace University.Data.Models
{
    public class Student
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
        [Range(StudentGPAMinValue, StudentGPAMaxValue)]
        public double GPA { get; set; }

        [Required]
        [Range(StudentYearMinValue, StudentYearMaxValue)]
        public int Year { get; set; }
    }
}
