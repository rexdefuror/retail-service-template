using MassTransit;
using Retail.Contracts;
using System;
using System.Threading.Tasks;

namespace Retail.FakeTrigger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = "rabbitmq://localhost";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(host));
            });

            await bus.StartAsync();

            while (true)
            {
                Console.Write("\nEnter text: ");
                var text = Console.ReadLine();


                await bus.Publish(new TemplateEventOccurred { Content = text });
            }
        }
    }
}
