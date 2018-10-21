using Memby.Core.Enums;
using System;

namespace Memby.Application.Users
{
    public class UserDto
    {
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Genders Gender { get; set; }

        public bool IsIndividualOffersEnabled { get; set; }

        public bool IsNewOffersEnabled { get; set; }

        public bool IsSystemNotificationsEnabled { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid Uuid { get; set; }
    }
}
