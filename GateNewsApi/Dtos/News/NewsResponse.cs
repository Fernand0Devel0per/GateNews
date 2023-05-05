using GateNewsApi.Dtos.Authors;

namespace GateNewsApi.Dtos.News
{
    public class NewsResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishDate { get; set; }
        public AuthorNewsResponse Author { get; set; }
        public string Category { get; set; }
    }
}
