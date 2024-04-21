using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.Data.Configuration;
using University.Data.Models;

namespace University.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EventParticipant>().HasKey(k => new { k.UserId, k.EventId });
            builder.Entity<EventParticipant>()
                .HasOne(e => e.Event)
                .WithMany(e => e.EventParticipants)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LectureParticipant>().HasKey(k => new { k.StudentId, k.LectureId });
            builder.Entity<LectureParticipant>()
                .HasOne(e => e.Lecture)
                .WithMany(e => e.LectureParticipants)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new SubjectConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventParticipant> EventsParticipants { get; set; } = null!;
        public DbSet<Lecture> Lectures { get; set; } = null!;
        public DbSet<LectureParticipant> LecturesParticipants { get; set; } = null!;
        public DbSet<Professor> Professors { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
    }
}