namespace AsbClient
{
    using System.Threading.Tasks;

    using Models;
    
    using NServiceBus;
    using NServiceBus.Logging;

    class MessageHandler : IHandleMessages<Message>
    {
        private static readonly ILog Log = LogManager.GetLogger<MessageHandler>();
        public async Task Handle(Message message, IMessageHandlerContext context)
        {
            Log.Info($"Received message with Id: {message.Id}");

           await context.Publish(new Event { Id = message.Id }).ConfigureAwait(false);

           await context.Reply(new Reply { Id = message.Id }).ConfigureAwait(false);

           //await context.Send(new Message { Id = message.Id });
        }
    }
}
