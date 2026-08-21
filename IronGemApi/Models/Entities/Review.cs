namespace IronGemApi.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public string? CommentText { get; set; }

        public int UserId { get; set; }

        public int CourseId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties

        public ApplicationUser User { get; set; } = null!;

        public Course Course { get; set; } = null!;
    }
}