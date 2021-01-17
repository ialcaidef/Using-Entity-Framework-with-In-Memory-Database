
using Microsoft.EntityFrameworkCore;
using InMemory.Dal.Models;

namespace InMemory.Dal.Database
{
    public class SchoolContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public SchoolContext()
        {

        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(x => x.Students);
            modelBuilder.Entity<Student>().HasMany(x => x.Courses);
        }
    }
}