using RawRabbit.Configuration;

namespace D.MoveOn.Common.RabbitMQ
{
    public class RabbitMqOptions : RawRabbitConfiguration
    {
        public string Namespace { get; set; }
        public int Retries { get; set; }
        public int RetryInterval { get; set; }
    }
}
