namespace IronGemApi.Models.DTOs.Common
{
    public class ApiResponseDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}