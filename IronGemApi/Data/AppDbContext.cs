using IronGemApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IronGemApi.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureApplicationUser(modelBuilder);
            ConfigureCourses(modelBuilder);
            ConfigureEnrollments(modelBuilder);
            ConfigureReviews(modelBuilder);
            ConfigureOffers(modelBuilder);
        }

        private static void ConfigureApplicationUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private static void ConfigureCourses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(c => c.Price)
                    .HasPrecision(10, 2)
                    .IsRequired();

                entity.Property(c => c.IsActive)
                    .HasDefaultValue(true);

                entity.Property(c => c.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(c => c.Coach)
                    .WithMany(u => u.Courses)
                    .HasForeignKey(c => c.CoachId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureEnrollments(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.EnrolledAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(e => new
                {
                    e.UserId,
                    e.CourseId
                }).IsUnique();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Enrollments)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureReviews(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.CommentText)
                    .HasMaxLength(1000);

                entity.Property(r => r.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(r => new
                {
                    r.UserId,
                    r.CourseId
                }).IsUnique();

                entity.HasOne(r => r.User)
                    .WithMany(u => u.Reviews)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Course)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(r => r.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureOffers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(o => o.DiscountPercent)
                    .HasPrecision(5, 2)
                    .IsRequired();

                entity.HasOne(o => o.Course)
                    .WithMany(c => c.Offers)
                    .HasForeignKey(o => o.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}