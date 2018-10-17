namespace Memby.Application.Users
{
    public class LoginUserResultDto
    {
        public string AuthTicket { get; set; }

        public UserDto User { get; set; }
    }
}
