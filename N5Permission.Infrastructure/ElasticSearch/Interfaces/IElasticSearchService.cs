

using Elastic.Clients.Elasticsearch;

namespace N5Permission.Infrastructure.ElasticSearch.Interfaces
{
    public interface IElasticSearchService
    {
        Task IndexDocumentAsync<T>(string indexName, string id, T document);
        Task<T?> GetDocumentAsync<T>(string indexName, string id);
        Task<bool> DeleteDocumentAsync(string indexName, string id);
        Task<bool> IndexExistsAsync(string indexName);
        Task<bool> UpdateDocumentAsync<T>(string indexName, string id, T document);
        Task CreateIndexAsync(string indexName, string settingsJson);
        Task<SearchResponse<T>> SearchAsync<T>(string indexName, SearchRequest searchRequest) where T : class;



    }
}
