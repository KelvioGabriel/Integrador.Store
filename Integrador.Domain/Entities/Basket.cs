using Integrador.Core.DomainObjects;
using Integrador.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Domain.Entities
{
    public class Basket : Entity
    {
        protected Basket() { }
        public Basket(Client client, Guid clientId)
        {
            Client = client;
            ClientId = clientId;
            //Inicilizando uma nova Lista
            Items = new List<BasketItem>();
            Active = true;
        }

        public Guid ClientId { get; private set; }
        [NotMapped]
        public Client Client { get; private set; }
        public List<BasketItem> Items { get; private set; }
        public bool Active { get; private set; }

        public void SetClient(Client client)
        {
            if(client != null)
            {
                ClientId = client.Id;
            }

            Client = client;
        }

        public void AddItem(BasketItem item)
        {
            Items.Add(item);
        }
        public void RemoveItem(BasketItem item)
        {
            Items.Remove(item);
        }

        public void ClearBasket()
        {
            Items = new List<BasketItem>();
        }

        public void SetUpdateAmountItem(BasketItem item, int newAmount)
        {
            Items.Where(i => i.Id == item.Id).First().SetAmount(newAmount);
        }

        public void InactiveBasket()
        {
            Active = false;
        }
    }
}
