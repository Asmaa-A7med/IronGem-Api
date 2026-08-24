using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.Entities;

namespace IronGemApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<JwtTokenDto> GenerateTokenAsync(ApplicationUser user);
    }
}