namespace Router
{
    using System;
    using System.Threading.Tasks;
    
    using NServiceBus;
    using NServiceBus.Router;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "NServiceBusRouter";

            var routerConfig = new RouterConfiguration("NServiceBusRouter");

            var msmqInterface = routerConfig.AddInterface<MsmqTransport>("MSMQ", t => { });
            msmqInterface.EnableMessageDrivenPublishSubscribe(new InMemorySubscriptionStorage());

            var asbInterface = routerConfig.AddInterface<AzureServiceBusTransport>("ASB", t =>
            {
                t.ConnectionString("Endpoint=sb://testnsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=5r6Thm5CY/egHO1zrJV4+Tuu6wjk3oMPjqp57wfJVtQ=");
            });

            var staticRouting = routerConfig.UseStaticRoutingProtocol();
            staticRouting.AddForwardRoute("MSMQ", "ASB");
            staticRouting.AddForwardRoute("ASB", "MSMQ");
            routerConfig.AutoCreateQueues();

            var router = Router.Create(routerConfig);

            await router.Start().ConfigureAwait(false);

            Console.WriteLine("Router is running, Press any key to stop the service");
            Console.ReadKey();

            await router.Stop().ConfigureAwait(false);
        }
    }
}