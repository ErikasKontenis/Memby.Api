using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memby.Services.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly SecurityOptions _securityOptions;

        public SecurityService(IOptions<SecurityOptions> securityOptions)
        {
            _securityOptions = securityOptions.Value;
        }

        public string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = Encoding.ASCII.GetBytes(_securityOptions.PasswordSalt);

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
