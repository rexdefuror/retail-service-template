namespace Retail.Service.Options
{
    public class MassTransitOptions
    {
        public const string Section = "MassTransit";

        public string RabbitHost { get; set; }
        public string RabbitQueue { get; set; }
        public double CancellationTokenDelay { get; set; }
    }
}
