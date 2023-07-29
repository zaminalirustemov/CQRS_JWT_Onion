﻿using MediatR;

namespace CQRS_JWTApp.API.Core.Application.Features.CQRS.Commands
{
    public class UpdateProductCommandRequest : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
