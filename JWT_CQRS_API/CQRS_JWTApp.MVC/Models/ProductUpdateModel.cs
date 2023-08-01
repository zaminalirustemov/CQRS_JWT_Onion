using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CQRS_JWTApp.MVC.Models
{
    public class ProductUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Price { get; set; }
        public SelectList? Categories { get; set; }
    }
}
