using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Memby.Services.Jwt
{
    public interface IJwtFactory
    {
        Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string email);

        Task<string> GenerateEncodedToken(string email, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string email, string id);
    }
}
