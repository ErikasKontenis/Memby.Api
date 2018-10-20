using System;
using System.Collections.Generic;
using System.Text;

namespace Memby.Services.Security
{
    public interface ISecurityService
    {
        string HashPassword(string password);
    }
}
