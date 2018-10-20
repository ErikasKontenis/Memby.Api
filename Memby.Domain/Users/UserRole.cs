namespace Memby.Domain.Users
{
    public class UserRole : Entity
    {
        public int RoleId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}
