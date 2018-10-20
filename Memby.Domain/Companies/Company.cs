using Memby.Domain.Employees;
using Memby.Domain.Users;
using System.Collections.Generic;

namespace Memby.Domain.Companies
{
    public class Company : Entity
    {
        public Company()
        {
            Employees = new List<Employee>();
        }

        public string Address { get; set; }

        public string BrandName { get; set; }

        public List<Employee> Employees { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string VatNumber { get; set; }
    }
}
