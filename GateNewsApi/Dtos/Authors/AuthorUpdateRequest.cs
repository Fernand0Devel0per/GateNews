namespace GateNewsApi.Dtos.Authors
{
    public class AuthorUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
    }
}
