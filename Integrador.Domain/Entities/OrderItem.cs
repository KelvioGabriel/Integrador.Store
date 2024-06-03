using Integrador.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product,
            decimal unitPrice, 
            int amount, 
            Order order)
        {
            OrderId = order.Id;
            ProductId = product.Id;
            ProductName = product.Name;
            UnitPrice = unitPrice;
            Amount = amount;
            ProductImage = product.Image;
            Order = order;
        }

        protected OrderItem() { }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set;}
        public decimal UnitPrice { get; private set; }
        public int Amount { get; private set; }
        public string ProductImage { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public void SetAddAmount(int amount)
        {
            Amount += amount;
        }

        public decimal TotalValueItem()
        {
            return UnitPrice * Amount;
        }
    }
}
