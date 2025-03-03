

namespace N5Permission.Infrastructure.Kafka.Models
{
    public sealed record KafkaSetting
    {
        public string Topic { get; set; }
        public string Url { get; set; }
    }
}
