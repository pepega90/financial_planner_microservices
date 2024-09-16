namespace SharedService.Messaging.Messages
{
    public class UserCreatedMessage
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
