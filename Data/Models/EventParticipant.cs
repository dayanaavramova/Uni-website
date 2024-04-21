using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.Data.Models
{
    public class EventParticipant
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
