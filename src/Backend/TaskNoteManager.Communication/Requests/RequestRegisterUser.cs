using TaskNoteManager.Communication.Enums;

namespace TaskNoteManager.Communication.Requests
{
    public class RequestRegisterUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Position Position { get; set; }
        public UserType UserType { get; set; }
        public Guid UserIdentiier { get; set; } = Guid.NewGuid();
    }
}
