namespace Models
{
    using NServiceBus;

    public class Event : IEvent
    {
        public string Id { get; set; }
    }
}
