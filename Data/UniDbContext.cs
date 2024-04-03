using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.Data.Models;

namespace University.Data
{
    public class UniDbContext : IdentityDbContext
    {
        public UniDbContext(DbContextOptions<UniDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set;}
        public DbSet<Subject> Subjects { get; set; }

    }
}