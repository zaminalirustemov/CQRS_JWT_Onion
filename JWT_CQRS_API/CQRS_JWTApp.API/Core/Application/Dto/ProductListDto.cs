﻿namespace CQRS_JWTApp.API.Core.Application.Dto
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
