namespace MsmqClient 
{
    using System.Threading.Tasks;
    
    using Models;
    
    using NServiceBus;
    using NServiceBus.Logging;

    class MessageHandler : IHandleMessages<Message>
    {
        private static readonly ILog Log = LogManager.GetLogger<MessageHandler>();
        public Task Handle(Message message, IMessageHandlerContext context)
        {
            Log.Info($"Received a message from AsbClient with id: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
