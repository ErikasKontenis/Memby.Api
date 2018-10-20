using Memby.Application.Users;
using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Core.Enums;
using Memby.Core.Exceptions;
using Memby.Core.Resources;
using Memby.Domain.Users;
using Memby.Services.Jwt;
using Memby.Services.Security;
using System.Threading.Tasks;

namespace Memby.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly ISecurityService _securityService;

        public UsersService(IUsersRepository usersRepository,
            IJwtFactory jwtFactory,
            ISecurityService securityService)
        {
            _usersRepository = usersRepository;
            _jwtFactory = jwtFactory;
            _securityService = securityService;
        }

        public async Task<CreateUserResultDto> Register(CreateUserDto createUserDto)
        {
            if (await _usersRepository.GetAsync(filter: o => o.Email == createUserDto.Email) != null)
            {
                throw new ValidationException(Messages.EmailExists, nameof(Messages.EmailExists));
            }

            var user = new User()
            {
                DateOfBirth = createUserDto.DateOfBirth,
                Email = createUserDto.Email,
                GenderId = (int)createUserDto.Gender,
                IsIndividualOffersEnabled = createUserDto.IsIndividualOffersEnabled,
                IsNewOffersEnabled = createUserDto.IsNewOffersEnabled,
                IsSystemNotificationsEnabled = createUserDto.IsSystemNotificationsEnabled,
                Name = createUserDto.Name,
                Password = _securityService.HashPassword(createUserDto.Password),
                Surname = createUserDto.Surname
            };

            user.UserRoles.Add(new UserRole() { RoleId = (int)Roles.NaturalPerson });

            return new CreateUserResultDto()
            {
                UserId = (await _usersRepository.InsertAsync(user)).Id
            };
        }

        public async Task<LoginUserResultDto> Login(LoginUserDto loginUserDto)
        {
            var user = await _usersRepository.GetAsync(filter: o => o.Email == loginUserDto.Email && o.Password == _securityService.HashPassword(loginUserDto.Password), includeProperties: "UserRoles");
            if (user == null)
            {
                throw new ValidationException(Messages.LoginUserIncorrectCredentials, nameof(Messages.LoginUserIncorrectCredentials));
            }

            var userClaimsIdentity = await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(user));

            var jwt = await _jwtFactory.GenerateJwt(userClaimsIdentity, _jwtFactory, loginUserDto.Email);

            var userDto = new UserDto()
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = (Genders)user.GenderId,
                IsIndividualOffersEnabled = user.IsIndividualOffersEnabled,
                IsNewOffersEnabled = user.IsNewOffersEnabled,
                IsSystemNotificationsEnabled = user.IsSystemNotificationsEnabled,
                Name = user.Name,
                Surname = user.Surname
            };

            return new LoginUserResultDto()
            {
                AuthTicket = jwt,
                User = userDto
            };
        }

        public async Task<UpdateUserInfoResultDto> UpdateUserInfo(UpdateUserInfoDto updateUserInfoDto, int userId)
        {
            var user = await _usersRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ValidationException(Messages.UserDoesNotExist, nameof(Messages.UserDoesNotExist));
            }

            user.DateOfBirth = updateUserInfoDto.DateOfBirth;
            user.GenderId = (int)updateUserInfoDto.Gender;
            user.IsIndividualOffersEnabled = updateUserInfoDto.IsIndividualOffersEnabled;
            user.IsNewOffersEnabled = updateUserInfoDto.IsNewOffersEnabled;
            user.IsSystemNotificationsEnabled = updateUserInfoDto.IsSystemNotificationsEnabled;
            user.Name = updateUserInfoDto.Name;
            user.Surname = updateUserInfoDto.Surname;

            await _usersRepository.UpdateAsync(user);

            return new UpdateUserInfoResultDto();
        }

        public async Task<UpdateUserEmailResultDto> UpdateUserEmail(UpdateUserEmailDto updateUserEmailDto, int userId)
        {
            var user = await _usersRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ValidationException(Messages.UserDoesNotExist, nameof(Messages.UserDoesNotExist));
            }

            if (await _usersRepository.GetAsync(filter: o => o.Email == updateUserEmailDto.Email) != null)
            {
                throw new ValidationException(Messages.EmailExists, nameof(Messages.EmailExists));
            }

            user.Email = updateUserEmailDto.Email;

            await _usersRepository.UpdateAsync(user);

            return new UpdateUserEmailResultDto();
        }

        public async Task<UpdateUserPasswordResultDto> UpdateUserPassword(UpdateUserPasswordDto updateUserPasswordDto, int userId)
        {
            var user = await _usersRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ValidationException(Messages.UserDoesNotExist, nameof(Messages.UserDoesNotExist));
            }

            if (user.Password != _securityService.HashPassword(updateUserPasswordDto.OldPassword))
            {
                throw new ValidationException(Messages.UpdateUserPasswordIncorrectOldPassword, nameof(Messages.UpdateUserPasswordIncorrectOldPassword));
            }

            user.Password = _securityService.HashPassword(updateUserPasswordDto.Password);

            await _usersRepository.UpdateAsync(user);

            return new UpdateUserPasswordResultDto();
        }
    }
}
