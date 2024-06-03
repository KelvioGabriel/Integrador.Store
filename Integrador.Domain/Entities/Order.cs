using Integrador.Core.DomainObjects;
using Integrador.Core.Enums;

namespace Integrador.Domain.Entities
{
    public class Order : Entity
    {
        const int SIZE_CODE = 5;
        protected Order() { }

        public Order(decimal discount,
            Client client,
            Address address,
            PaymentMethod paymentMethod)
        {
            ClientId = client.Id;
            Discount = discount;
            AddressId = address.Id;
            Status = OrderStatus.Em_aprovação;
            Client = client;
            Address = address;
            PaymentMethod = paymentMethod;
            PaymentMethodId = paymentMethod.Id;
            Discount = 0;
            Code = GetRandomAlphanumeric(SIZE_CODE);

        }

        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public decimal Discount { get; private set; }
        public Guid AddressId { get; private set; }
        public OrderStatus Status { get; private set; }

        public Client Client { get; private set; }
        public Voucher? Voucher { get; private set; }
        public Address Address { get; private set; }
        public Guid PaymentMethodId { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal DiscountValue { get; private set; }
        public decimal FinalValue { get; private set; }
        public string Code { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(OrderItem item)
        {
            if(_orderItems.Where(oi => oi.Id == item.Id).Any())
            {
                if(item.Amount != 0)
                {
                    _orderItems.FirstOrDefault(oi => oi.Id == item.Id)
                        .SetAddAmount(item.Amount);
                }
                else
                {
                    _orderItems.RemoveAll(oi => oi.Id == item.Id);
                }
            }
            else
            {
                _orderItems.Add(item);
            }
        }
        public void SetPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
            PaymentMethodId = PaymentMethod.Id;
        }

        public void SetAddress(Address address)
        {
            Address = address;
            AddressId = address.Id;
        }

        public void SetVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherId = voucher.Id;

            CalculateValueTotalDiscount();
        }

        public void CalculateOrderValue()
        {
            if(OrderItems == null || OrderItems.Count == 0)
            {
                TotalValue = 0;
            }
            else
            {
                TotalValue = OrderItems.Sum(oi => oi.TotalValueItem());
            }
            CalculateValueTotalDiscount();
        }

        private void CalculateValueTotalDiscount()
        {
            decimal discount = 0;
            decimal valueTotalOrder = TotalValue;

            if(Voucher != null)
            {
                if(Voucher.DiscountType == DiscountTypeVoucher.Percentual)
                {
                    discount = (valueTotalOrder * Voucher.Value) / 100;
                }
                else
                {
                    discount = Voucher.Value;
                }
            }

            valueTotalOrder -= discount;

            if(valueTotalOrder < 0)
            {
                valueTotalOrder = 0;
            }

            FinalValue = valueTotalOrder;
            Discount = discount;
        }

        private string GetRandomAlphanumeric(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                            .Select(c => c[random.Next(c.Length)])
                            .ToArray());
            return result;
        }

        public void AuthorizedOrder() =>
            Status = OrderStatus.Aprovado;

        public void CanceledOrder() =>
            Status = OrderStatus.Cancelado;

        public void FinalizedOrder() =>
            Status = OrderStatus.Aprovado;

        public void ProcessedOrder() =>
            Status = OrderStatus.Em_aprovação;

        public void DeniedOrder() =>
            Status = OrderStatus.Recusado;

        public void DeliveredOrder() =>
            Status = OrderStatus.Entregue;

        public void SetFinalValue(decimal value)
        {
            FinalValue = value;
        }

        public void SetDiscountValue(decimal value)
        {
            DiscountValue = value;
        }
    }
}
