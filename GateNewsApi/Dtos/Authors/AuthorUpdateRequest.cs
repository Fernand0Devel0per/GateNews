using System.ComponentModel.DataAnnotations;

namespace GateNewsApi.Dtos.Authors
{
    public class AuthorUpdateRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public bool? IsActive { get; set; }
    }
}
