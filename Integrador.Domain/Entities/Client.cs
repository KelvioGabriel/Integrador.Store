using Integrador.Core.DomainObjects;

namespace Integrador.Domain.Entities
{
    public class Client : Entity
    {
        public Client(string name,
            string email,
            string cpf,
            bool active,
            DateTime birth,
            Address addressClient)
        {
            Name = name;
            Email = email;
            Cpf = cpf;
            Active = active;
            Birth = birth;
            AddressId = addressClient.Id;
            AddressClient = addressClient;
        }

        protected Client() { }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Active { get; private set; }
        public DateTime Birth { get; private set; }

        public Guid AddressId { get; private set; }
        public Address AddressClient { get; private set; }

        private List<PaymentMethod> _paymentMethods = new List<PaymentMethod>();
        public IEnumerable<PaymentMethod> PaymentMethods => _paymentMethods;

        public void SetAddress(Address address)
        {
            if(address != null)
            {
                AddressId = address.Id;
            }
            
            AddressClient = address;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }
    }
}
