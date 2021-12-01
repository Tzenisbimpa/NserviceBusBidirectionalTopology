namespace MsmqClient
{
    using System.Threading.Tasks;
    
    using Models;
    
    using NServiceBus;
    using NServiceBus.Logging;

    class ReplyHandler : IHandleMessages<Reply>
    {
        private static readonly ILog Log = LogManager.GetLogger<ReplyHandler>();

        public Task Handle(Reply message, IMessageHandlerContext context)
        {
            Log.Info($"Received a reply from Asb with Id: {message.Id}");
            return Task.CompletedTask;
        }
    }
}
