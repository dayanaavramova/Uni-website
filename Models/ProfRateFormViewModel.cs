using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;

namespace University.Models
{
    public class ProfRateFormViewModel
    {

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(ProfRatingMinValue, ProfRatingMaxValue)]
        public double Rating { get; set; }
    }
}
