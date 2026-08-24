using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.DTOs.Users;
using IronGemApi.Models.DTOs.Common;

namespace IronGemApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto?> GetCurrentUserAsync(int userId);
        Task<ApiResponseDto> UpdateProfileAsync(int userId, UpdateProfileDto updateProfileDto);
        Task<ApiResponseDto> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
    }
}