namespace MsmqClient
{
    using System.Threading.Tasks;
    
    using Models;

    using NServiceBus;
    using NServiceBus.Logging;

    class EventHandler : IHandleMessages<Event>
    {
        private static readonly ILog Log = LogManager.GetLogger<EventHandler>();

        public Task Handle(Event message, IMessageHandlerContext context)
        {
            Log.Info($"An event was published from Asb with Id: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
