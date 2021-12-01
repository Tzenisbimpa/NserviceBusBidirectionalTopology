namespace Models
{
    using NServiceBus;

    public class Message : IMessage
    {
        public string Id { get; set; }
    }
}
