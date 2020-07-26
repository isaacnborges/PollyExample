using System;

namespace WebPollyExample.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public int Code { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Product(Guid id, int code, string description, decimal price)
        {
            Id = id;
            Code = code;
            Description = description;
            Price = price;
        }
    }
}
