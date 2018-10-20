using Memby.Domain.Companies;
using Memby.Domain.Users;

namespace Memby.Domain.Employees
{
    public class Employee : Entity
    {
        public Company Company { get; set; }

        public int CompanyId { get; set; }

        public string Position { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
