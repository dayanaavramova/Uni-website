using System.ComponentModel.DataAnnotations;
using static University.Data.Constants.DataConstants;

namespace University.Models
{
    public class EventAllViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
    }
}
