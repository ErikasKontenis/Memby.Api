namespace Memby.Domain.Users
{
    public class UserProvider : Entity
    {
        public int ProviderId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string Uuid { get; set; }
    }
}
