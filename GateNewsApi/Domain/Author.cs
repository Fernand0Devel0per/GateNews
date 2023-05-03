namespace GateNewsApi.Domain
{
    public class Author
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<News> News { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
