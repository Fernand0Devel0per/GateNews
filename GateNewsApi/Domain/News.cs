namespace GateNewsApi.Domain
{
    public class News
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public Guid UserId { get; set; }

        public Author Author { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
