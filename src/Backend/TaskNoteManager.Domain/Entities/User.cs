using TaskNoteManager.Domain.Enums;

namespace TaskNoteManager.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Position Position { get; set; }
        public UserType UserType { get; set; }
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }
}
