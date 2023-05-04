using GateNewsApi.Enums;

namespace GateNewsApi.Dtos.News
{
    public class NewsCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
