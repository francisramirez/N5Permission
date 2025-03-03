namespace N5Permission.Infrastructure.ElasticSearch.Models
{
    public sealed record ElasticSearchSetting
    {
        public string Index { get; set; }
        public string Uri { get; set; }

    }
}
