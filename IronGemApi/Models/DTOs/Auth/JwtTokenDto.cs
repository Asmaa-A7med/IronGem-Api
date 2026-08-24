namespace IronGemApi.Models.DTOs.Auth
{
    public class JwtTokenDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}