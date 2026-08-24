namespace IronGemApi.Models.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = null!;

        public string? Token { get; set; }

        public DateTime? Expiration { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}