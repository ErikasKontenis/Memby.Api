using Memby.Application.Users;
using System.Threading.Tasks;

namespace Memby.Contracts.Services
{
    public interface IUsersService
    {
        Task Register(UpsertUserDto upsertUserDto);

        Task<LoginUserResultDto> Login(LoginUserDto loginUserDto);
    }
}
