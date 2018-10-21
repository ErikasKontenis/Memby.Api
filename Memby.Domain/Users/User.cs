using Memby.Domain.Companies;
using Memby.Domain.Employees;
using System;
using System.Collections.Generic;

namespace Memby.Domain.Users
{
    public class User : Entity
    {
        public User()
        {
            Companies = new List<Company>();
            Employees = new List<Employee>();
            UserProviders = new List<UserProvider>();
            UserRoles = new List<UserRole>();
        }

        public List<Company> Companies { get; set; }

        public string Email { get; set; }

        public List<Employee> Employees { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int GenderId { get; set; }

        public bool IsIndividualOffersEnabled { get; set; }

        public bool IsNewOffersEnabled { get; set; }

        public bool IsSystemNotificationsEnabled { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public List<UserProvider> UserProviders { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public Guid Uuid { get; set; }
    }
}
