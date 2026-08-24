using IronGemApi.Models.Entities;

namespace IronGemApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}