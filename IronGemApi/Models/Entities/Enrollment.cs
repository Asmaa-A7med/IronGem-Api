namespace IronGemApi.Models.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime EnrolledAt { get; set; }

        public string Status { get; set; } = null!;

        // Navigation Properties

        public ApplicationUser User { get; set; } = null!;

        public Course Course { get; set; } = null!;
    }
}