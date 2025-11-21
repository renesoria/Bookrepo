using Books_Auth.Models.DTOS;

namespace Books_Auth.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<(bool ok, LoginResponseDto? response)> LoginAsync(LoginDto dto);
        Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto);
        Task<bool> RevokeRefreshTokenAsync(RefreshRequestDto dto);
    }
}
