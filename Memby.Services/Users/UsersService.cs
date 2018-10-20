using Memby.Application.Users;
using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Core.Enums;
using Memby.Core.Exceptions;
using Memby.Core.Resources;
using Memby.Domain.Users;
using Memby.Services.Jwt;
using Memby.Services.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
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
                Gender = (int)createUserDto.Gender,
                IsIndividualOffersEnabled = createUserDto.IsIndividualOffersEnabled,
                IsNewOffersEnabled = createUserDto.IsNewOffersEnabled,
                IsSystemNotificationsEnabled = createUserDto.IsSystemNotificationsEnabled,
                Name = createUserDto.Name,
                Password = _securityService.HashPassword(createUserDto.Password),
                Surname = createUserDto.Surname
            };

            return new CreateUserResultDto()
            {
                UserId = (await _usersRepository.InsertAsync(user)).Id
            };
        }

        public async Task<LoginUserResultDto> Login(LoginUserDto loginUserDto)
        {
            var user = await _usersRepository.GetAsync(filter: o => o.Email == loginUserDto.Email && o.Password == _securityService.HashPassword(loginUserDto.Password));
            if (user == null)
            {
                throw new ValidationException(Messages.LoginUserIncorrectCredentials, nameof(Messages.LoginUserIncorrectCredentials));
            }

            var userClaimsIdentity = await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(user.Email, user.Id.ToString()));

            var jwt = await _jwtFactory.GenerateJwt(userClaimsIdentity, _jwtFactory, loginUserDto.Email);

            var userDto = new UserDto()
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Gender = (Genders)user.Gender,
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
            user.Gender = (int)updateUserInfoDto.Gender;
            user.IsIndividualOffersEnabled = updateUserInfoDto.IsIndividualOffersEnabled;
            user.IsNewOffersEnabled = updateUserInfoDto.IsNewOffersEnabled;
            user.IsSystemNotificationsEnabled = updateUserInfoDto.IsSystemNotificationsEnabled;
            user.Name = updateUserInfoDto.Name;
            user.Surname = updateUserInfoDto.Surname;

            await _usersRepository.UpdateAsync(user);

            return new UpdateUserInfoResultDto();
        }
    }
}
