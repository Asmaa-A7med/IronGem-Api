using System.ComponentModel.DataAnnotations;

namespace IronGemApi.Models.DTOs.Users
{
    public class UpdateProfileDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public DateOnly BirthDate { get; set; }
    }
}