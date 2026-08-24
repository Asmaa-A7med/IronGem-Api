using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.DTOs.Users;

namespace IronGemApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto?> GetCurrentUserAsync(int userId);
    }
}