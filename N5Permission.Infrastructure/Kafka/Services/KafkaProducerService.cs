

using Confluent.Kafka;
using Microsoft.Extensions.Options;
using N5Permission.Infrastructure.Kafka.Interfaces;
using N5Permission.Infrastructure.Kafka.Models;
using System.Text.Json;

namespace N5Permission.Infrastructure.Kafka.Services
{
    public sealed class KafkaProducerService : IKafkaProducerService
    {
       
        private readonly KafkaSetting _kafkaSetting;
        public KafkaProducerService(IOptions<KafkaSetting> kafkaConfigSetting)
        {
            _kafkaSetting = kafkaConfigSetting.Value;
        }
        public async Task ProduceAsync(OperationMessage message)
        {
            var config = new ProducerConfig { BootstrapServers = _kafkaSetting.Url };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var jsonMessage = JsonSerializer.Serialize(message);
            await producer.ProduceAsync(_kafkaSetting.Topic, new Message<Null, string> { Value = jsonMessage });
            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}
