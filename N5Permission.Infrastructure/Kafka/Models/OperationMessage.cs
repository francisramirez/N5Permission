

namespace N5Permission.Infrastructure.Kafka.Models
{
    public record  OperationMessage
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; } = string.Empty;
    }
}
