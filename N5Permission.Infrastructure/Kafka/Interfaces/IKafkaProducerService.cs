using System.Threading.Tasks;
using N5Permission.Infrastructure.Kafka.Models;
namespace N5Permission.Infrastructure.Kafka.Interfaces
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(OperationMessage message);
    }
}
