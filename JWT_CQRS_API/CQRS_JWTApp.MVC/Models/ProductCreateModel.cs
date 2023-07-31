using Microsoft.AspNetCore.Mvc.Rendering;

namespace CQRS_JWTApp.MVC.Models
{
    public class ProductCreateModel
    {
        public int CategoryId { get; set; }

        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public SelectList? Categories { get; set; }
    }
}
