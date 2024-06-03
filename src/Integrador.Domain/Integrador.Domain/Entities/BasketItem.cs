using Integrador.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Entities
{
    public class BasketItem : Entity
    {
        public BasketItem(Product product, Basket basket, int amount)
        {
            ProductId = product.Id;
            Product = product;
            BasketId = basket.Id;
            Basket = basket;
            Amount = amount;
        }

        protected BasketItem() { }
        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public Guid BasketId { get; private set; }
        public int Amount { get; private set; }
        public Basket Basket { get; private set; }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }

        public void SetProduct(Product product)
        {
            if (product != null)
            {
                ProductId = product.Id;
            }
            Product = product;
        }
    }
}
