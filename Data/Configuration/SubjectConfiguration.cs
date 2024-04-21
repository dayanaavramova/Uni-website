using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Models;

namespace University.Data.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasData(CreateSubjects());
        }

        private List<Subject> CreateSubjects()
        {
            List<Subject> subjects = new List<Subject>()
            {
                new Subject()
                {
                    Id = 1,
                    Name = "IT"
                },

                new Subject()
                {
                    Id = 2,
                    Name = "Economics"
                },

                new Subject()
                {
                    Id = 3,
                    Name = "Management"
                }

             };

            return subjects;
        }
    }
}
