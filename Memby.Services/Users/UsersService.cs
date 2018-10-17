using Memby.Application.Users;
using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Core.Enums;
using Memby.Core.Exceptions;
using Memby.Core.Resources;
using Memby.Domain.Users;
using Memby.Services.Jwt;
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
        private readonly JwtIssuerOptions _jwtOptions;

        public UsersService(IUsersRepository usersRepository,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _usersRepository = usersRepository;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task Register(UpsertUserDto upsertUserDto)
        {
            if (await _usersRepository.GetAsync(filter: o => o.Email == upsertUserDto.Email) != null)
            {
                throw new ValidationException(Messages.EmailExists, nameof(Messages.EmailExists));
            }

            var user = new User()
            {
                DateOfBirth = upsertUserDto.DateOfBirth,
                Email = upsertUserDto.Email,
                Gender = (int)upsertUserDto.Gender,
                IsIndividualOffersEnabled = upsertUserDto.IsIndividualOffersEnabled,
                IsNewOffersEnabled = upsertUserDto.IsNewOffersEnabled,
                IsSystemNotificationsEnabled = upsertUserDto.IsSystemNotificationsEnabled,
                Name = upsertUserDto.Name,
                Password = HashPassword(upsertUserDto.Password),
                Surname = upsertUserDto.Surname
            };

            await _usersRepository.InsertAsync(user);
        }

        public async Task<LoginUserResultDto> Login(LoginUserDto loginUserDto)
        {
            var user = await _usersRepository.GetAsync(filter: o => o.Email == loginUserDto.Email && o.Password == HashPassword(loginUserDto.Password));
            if (user == null)
            {
                throw new ValidationException(Messages.LoginUserIncorrectCredentials, nameof(Messages.LoginUserIncorrectCredentials));
            }

            var userClaimsIdentity = await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(user.Email, user.Id.ToString()));

            var jwt = await _jwtFactory.GenerateJwt(userClaimsIdentity, _jwtFactory, loginUserDto.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

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

        private string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = Encoding.ASCII.GetBytes("takfLBF5xETaZXCmyvWL00eochy04XV4zrXrp9iYCvtXeRXK6gEyM17gZaSgDJtPxTnEH924ik8sKXQX1s1HY47xuxUiXTvRYyHO");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
