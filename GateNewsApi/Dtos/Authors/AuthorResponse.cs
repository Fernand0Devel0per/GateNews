﻿namespace GateNewsApi.Dtos.Authors
{
    public class AuthorResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
