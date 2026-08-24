using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.DTOs.Users;
using IronGemApi.Models.Entities;
using IronGemApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IronGemApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager , IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
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

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            var isPasswordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    loginDto.Password);

            if (!isPasswordValid)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password."
                };
            }

            var token = await _jwtService.GenerateTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Login successful.",
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60),
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<UserProfileDto?> GetCurrentUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Role = roles.FirstOrDefault()
            };
        }
    }
}