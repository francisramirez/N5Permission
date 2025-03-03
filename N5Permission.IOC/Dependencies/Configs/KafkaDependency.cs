using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Infrastructure.Kafka.Interfaces;
using N5Permission.Infrastructure.Kafka.Models;
using N5Permission.Infrastructure.Kafka.Services;

namespace N5Permission.IOC.Dependencies.Configs
{
    public static class KafkaDependency
    {
        public static void AddKafkaDependency(this WebApplicationBuilder builder) 
        {
            builder.Services.AddOptions<KafkaSetting>().BindConfiguration("KafkaSetting");
            builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
        }
    }
}
