using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;

namespace University.Models
{
    public class StudentFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLenght,
            MinimumLength = NameMinLenght,
            ErrorMessage = StringLenghtErrorMessage)]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLenght,
             MinimumLength = NameMinLenght,
             ErrorMessage = StringLenghtErrorMessage)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int Year { get; set; }
    }
}
