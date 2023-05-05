using GateNewsApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace GateNewsApi.Dtos.News
{
    public class NewsCreateRequest
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(5000, MinimumLength = 100, ErrorMessage = "Content must be between 100 and 5000 characters.")]
        public string Content { get; set; }

        public CategoryEnum Category { get; set; }
    }
}
