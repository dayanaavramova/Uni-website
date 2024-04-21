using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;

namespace University.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EventDescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateTime { get; set; }

        public IList<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
