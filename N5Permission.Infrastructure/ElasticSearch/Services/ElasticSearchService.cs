using System.Text.Json;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Nodes;
using N5Permission.Infrastructure.ElasticSearch.Interfaces;
using N5Permission.Infrastructure.ElasticSearch.Models;
using N5Permission.Infrastructure.Logger;
using static System.Net.Mime.MediaTypeNames;

namespace N5Permission.Infrastructure.ElasticSearch.Services
{
    public sealed class ElasticSearchService : IElasticSearchService
    {
        private ElasticsearchClient _client;
        public ElasticSearchService(ElasticsearchClient client) => _client = client;
        public async Task CreateIndexAsync(string indexName, string settingsJson)
        {
            var settings = JsonSerializer.Deserialize<IndexSettings>(settingsJson);

            var response = await _client.Indices.CreateAsync(indexName, c => c.Settings(settings));

            if (!response.IsSuccess())
            {
                throw new Exception($"Failed to create index: {response.DebugInformation}");
            }
        }
        public async Task<bool> DeleteDocumentAsync(string indexName, string id)
        {
            var request = new DeleteRequest(indexName, id);
            var response = await _client.DeleteAsync(request);
            return response.IsSuccess();
        }
        public async Task<T?> GetDocumentAsync<T>(string indexName, string id)
        {
          
            var response = await _client.GetAsync<T>(indexName, id);
            return response.Found ? response.Source : default;
        }
        public async Task IndexDocumentAsync<T>(string indexName, string id, T document)
        {
            var response = await _client.IndexAsync(document, indexName, id);
            if (!response.IsSuccess())
            {
                throw new Exception($"Failed to index document: {response.DebugInformation}");
            }
        }
        public async Task<bool> IndexExistsAsync(string indexName)
        {
            var response = await _client.Indices.ExistsAsync(indexName);
            return response.Exists;
        }
        public async Task<SearchResponse<T>> SearchAsync<T>(string indexName, SearchRequest searchRequest) where T : class
        {
            var response = await _client.SearchAsync<T>(searchRequest);
            return response;
        }

        public async Task<bool> UpdateDocumentAsync<T>(string indexName, string id, T document)
        {
            var updateRequest = new UpdateRequest<T, T>(indexName, id) { Doc = document };
            var response = await _client.UpdateAsync(updateRequest);
            return response.IsSuccess();
        }

       
    }
}
