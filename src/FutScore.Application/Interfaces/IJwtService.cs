using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface IJwtService
    {
        //string GenerateToken(User user);
        string GenerateRefreshToken();
    }
} 