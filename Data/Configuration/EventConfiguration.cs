using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using University.Data.Models;

namespace University.Data.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(CreateEvent());
        }

        private List<Event> CreateEvent()
        {
            var events = new List<Event>()
            {
                new Event()
                 {
                      Id = 1,
                      Name = "Career Fair",
                      Description = "Employers have been invited onto campus to offer internships or even jobs to students.",
                      DateTime = new DateTime(2024, 04, 21, 14, 00, 00)
                 },

                new Event()
                 {
                      Id = 2,
                      Name = "Game Night",
                      Description = "Everyone (even professors) is invited to a fun game night. Games are provided :)",
                      DateTime = new DateTime(2024, 05, 17, 19, 30, 00)
                 },

               new Event()
                 {
                      Id = 3,
                      Name = "Macroeconomics Student Conferece",
                      Description = "Our students will present their essays on macroeconomic problems during our annual conference.",
                      DateTime = new DateTime(2024, 05, 12, 13, 30, 00)
                 },
            };

            return events;
        }
    }
}
