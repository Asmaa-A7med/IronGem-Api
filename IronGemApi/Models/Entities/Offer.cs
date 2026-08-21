namespace IronGemApi.Models.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal DiscountPercent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }

        // Navigation Property

        public Course Course { get; set; } = null!;
    }
}