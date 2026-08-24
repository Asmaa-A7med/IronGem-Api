using IronGemApi.Models.DTOs.Auth;
using IronGemApi.Models.DTOs.Common;
using IronGemApi.Models.DTOs.Users;
using IronGemApi.Models.Entities;
using IronGemApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IronGemApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<ApiResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if email already exists
            var existingUser =
                await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                return new ApiResponseDto
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
                return new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description))
                };
            }

            // Assign default role
            await _userManager.AddToRoleAsync(user, "User");

            return new ApiResponseDto
            {
                IsSuccess = true,
                Message = "User registered successfully."
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return null;

            var passwordResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: true);

            if (!passwordResult.Succeeded)
            {
                return null;
            }
            if (passwordResult.IsLockedOut)
            {
                return null;
            }

            if (!passwordResult.Succeeded)
            {
                return null;
            }


            var jwtToken = await _jwtService.GenerateTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Token = jwtToken.Token,
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

        public async Task<ApiResponseDto> UpdateProfileAsync(int userId, UpdateProfileDto updateProfileDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            user.FirstName = updateProfileDto.FirstName;
            user.LastName = updateProfileDto.LastName;
            user.PhoneNumber = updateProfileDto.PhoneNumber;
            user.BirthDate = updateProfileDto.BirthDate;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description))
                };
            }

            return new ApiResponseDto
            {
                IsSuccess = true,
                Message = "Profile updated successfully."
            };
        }

        public async Task<ApiResponseDto> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                changePasswordDto.CurrentPassword,
                changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new ApiResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description))
                };
            }

            return new ApiResponseDto
            {
                IsSuccess = true,
                Message = "Password changed successfully."
            };
        }
    }
}