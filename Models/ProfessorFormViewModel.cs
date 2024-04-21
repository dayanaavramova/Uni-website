using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using University.Data.Models;
using static University.Data.Constants.DataConstants;

namespace University.Models
{
    public class ProfessorFormViewModel
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
        public int SubjectId { get; set; }

        public IEnumerable<SubjectViewModel> Subjects { get; set; } = new List<SubjectViewModel>();
    }
}
