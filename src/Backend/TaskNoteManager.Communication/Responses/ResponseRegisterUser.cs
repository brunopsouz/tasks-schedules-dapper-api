using TaskNoteManager.Communication.Enums;

namespace TaskNoteManager.Communication.Responses
{
    /// <summary>
    /// Class for responses information of user.
    /// </summary>
    public class ResponseRegisterUser
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Position Position { get; set; }
        public UserType UserType { get; set; }
    }
}
