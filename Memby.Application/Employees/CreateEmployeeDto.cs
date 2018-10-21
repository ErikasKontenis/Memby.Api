using System;

namespace Memby.Application.Employees
{
    public class CreateEmployeeDto
    {
        public int CompanyId { get; set; }

        public string Position { get; set; }

        public Guid UserUuid { get; set; }
    }
}
