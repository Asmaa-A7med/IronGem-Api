using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.Entities;
using IronGemApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IronGemApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> RegisterAsync(
            RegisterDto registerDto)
        {
            // Check if email already exists
            var existingUser =
                await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Email is already registered."
                };
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                BirthDate = registerDto.BirthDate,
                CreatedAt = DateTime.UtcNow
            };

            // Identity automatically hashes the password
            var result =
                await _userManager.CreateAsync(
                    user,
                    registerDto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description))
                };
            }

            // Assign default role
            await _userManager.AddToRoleAsync(user, "User");

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "User registered successfully."
            };
        }
    }
}