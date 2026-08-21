using IronGemApi.Models.DTOs.Auth;

namespace IronGemApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}