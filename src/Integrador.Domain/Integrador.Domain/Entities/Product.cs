using Integrador.Core.DomainObjects;
using Integrador.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Entities
{
    public class Product :  Entity
    {
        public Product(string name,
           string description,
           bool active,
           decimal price,
           string image,
           int stockQuantity)
        {
            Name = name;
            Description = description;
            Active = active;
            Price = price;
            Image = image;
            StockQuantity = stockQuantity;
            Tipo = ProductType.Indefinido;
        }

        protected Product() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public int StockQuantity { get; private set; }
        public ProductType Tipo { get; private set; }
        public void SetAddRemoveStock(int amount)
        {
            StockQuantity += amount;
        }
    }
}
