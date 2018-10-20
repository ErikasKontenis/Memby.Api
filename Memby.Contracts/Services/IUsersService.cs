using Memby.Application.Users;
using System.Threading.Tasks;

namespace Memby.Contracts.Services
{
    public interface IUsersService
    {
        Task<CreateUserResultDto> Register(CreateUserDto createUserDto);

        Task<LoginUserResultDto> Login(LoginUserDto loginUserDto);

        Task<UpdateUserInfoResultDto> UpdateUserInfo(UpdateUserInfoDto updateUserInfoDto, int userId);

        Task<UpdateUserEmailResultDto> UpdateUserEmail(UpdateUserEmailDto updateUserEmailDto, int userId);
    }
}
