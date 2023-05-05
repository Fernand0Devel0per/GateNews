using System.ComponentModel.DataAnnotations;

namespace GateNewsApi.Dtos.Authors
{
    public class AuthorUpdateRequest
    {
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^\S+.*$", ErrorMessage = "First name cannot be only spaces.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^\S+.*$", ErrorMessage = "Last name cannot be only spaces.")]
        public string LastName { get; set; }

        public bool? IsActive { get; set; }
    }
}
