

using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Permission.Infrastructure.ElasticSearch.Interfaces;
using N5Permission.Infrastructure.ElasticSearch.Models;
using N5Permission.Infrastructure.ElasticSearch.Services;

namespace N5Permission.IOC.Dependencies.Configs
{
    public static class ElasticSearchDependency
    {
        public static void AddElasticSearchDependency(this WebApplicationBuilder builder)
        {
            // Configure the Elasticsearch Config //
           builder.Services.AddOptions<ElasticSearchSetting>().BindConfiguration("Elasticsearch");

            // Configure the Elasticsearch client
            var settings = new ElasticsearchClientSettings(new Uri(builder.Configuration.Get<ElasticSearchSetting>().Uri))
                .DefaultIndex(builder.Configuration.Get<ElasticSearchSetting>().Index);
            
            var client = new ElasticsearchClient(settings);
            
            builder.Services.AddSingleton(client);

            builder.Services.AddSingleton<IElasticSearchService, ElasticSearchService>();

        }
    }
}
