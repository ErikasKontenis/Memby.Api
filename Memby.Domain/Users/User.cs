﻿using System;
using System.Collections.Generic;

namespace Memby.Domain.Users
{
    public class User : Entity
    {
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Gender { get; set; }

        public bool IsIndividualOffersEnabled { get; set; }

        public bool IsNewOffersEnabled { get; set; }

        public bool IsSystemNotificationsEnabled { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public List<UserProvider> UserProviders { get; set; }
    }
}
