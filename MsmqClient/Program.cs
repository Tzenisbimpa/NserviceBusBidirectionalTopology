namespace MsmqClient
{
    using System;
    using System.Threading.Tasks;
    
    using Models;
    
    using NServiceBus;

    class Program
    {
        static async Task Main(string[] args)
        {
            var rand = new Random();
            Console.Title = "MsmqClient";

            var endpointConfiguration = new EndpointConfiguration("MsmqClient");
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();

            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();

            var bridge = transport.Routing().ConnectToRouter("NServiceBusRouter");
            bridge.RouteToEndpoint(typeof(Message), "AsbClient");
            bridge.RegisterPublisher(typeof(Event), "AsbClient");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("MSMQ client service is running.\nPress 'Q' to stop the service or press any other key to send a message.");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Q)
                {
                    break;
                }
                await endpointInstance.Send(new Message { Id = rand.Next(100, 999).ToString() }).ConfigureAwait(false);
            }

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
