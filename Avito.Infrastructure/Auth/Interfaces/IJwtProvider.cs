using Avito.Logic.Models;

namespace Avito.Infrastructure.Auth.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}