namespace Models
{
    using NServiceBus;

    public class Reply : IMessage
    {
        public string Id { get; set; }
    }
}
