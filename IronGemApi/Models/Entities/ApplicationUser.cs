using Microsoft.AspNetCore.Identity;

namespace IronGemApi.Models.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties

        // Courses where this user is the coach
        public ICollection<Course> Courses { get; set; }
            = new List<Course>();

        // Courses where this user is enrolled
        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        // Reviews written by this user
        public ICollection<Review> Reviews { get; set; }
            = new List<Review>();
    }
}