using System.ComponentModel.DataAnnotations;

namespace CQRS_JWTApp.MVC.Models
{
    public class CategoryCreateModel
    {
        [Required]
        public string? Definition { get; set; } = null!;
    }
}
