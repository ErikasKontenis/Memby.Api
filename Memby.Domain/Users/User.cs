namespace Memby.Domain.Users
{
    public class User : Entity
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
