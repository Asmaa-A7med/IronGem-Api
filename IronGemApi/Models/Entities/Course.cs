namespace IronGemApi.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CoachId { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties

        public ApplicationUser Coach { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

        public ICollection<Review> Reviews { get; set; }
            = new List<Review>();

        public ICollection<Offer> Offers { get; set; }
            = new List<Offer>();
    }
}