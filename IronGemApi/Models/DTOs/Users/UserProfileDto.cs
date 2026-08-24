namespace IronGemApi.Models.DTOs.Users
{
    public class UserProfileDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public string? Role { get; set; }
    }
}