namespace Application.Features.Identity.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponse> LoginAsync(TokenRequest request);
        // RefreshToken
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
