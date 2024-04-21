using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;

namespace University.Data.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Professor> Professors { get; set; } = new List<Professor>();
    }
}
