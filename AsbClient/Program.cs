namespace AsbClient
{
    using System;
    using System.Threading.Tasks;
    
    using Models;
    
    using NServiceBus;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "AsbClient";
            var endpointConfiguration = new EndpointConfiguration("AsbClient");
            var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString("Endpoint=sb://testnsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5r6Thm5CY/egHO1zrJV4+Tuu6wjk3oMPjqp57wfJVtQ=");

            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();

           /* var bridge = transport.Routing().ConnectToRouter("NServiceBusRouter");
            bridge.RouteToEndpoint(typeof(Message), "MsmqClient");*/

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("ASB client service is running. Press any key to stop the service.");
            Console.ReadKey();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
