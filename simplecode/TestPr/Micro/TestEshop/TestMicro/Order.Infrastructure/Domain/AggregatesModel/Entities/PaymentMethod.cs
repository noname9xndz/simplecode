using Order.Infrastructure.Domain.Exceptions;
using Order.Infrastructure.Domain.Models;
using System;

namespace Order.Infrastructure.Domain.AggregatesModel.Entities
{
    public class PaymentMethod
       : Entity
    {
        private string _alias;
        private string _cardNumber;
        private string _securityNumber;
        private string _cardHolderName;
        private DateTime _expiration;

        private int _cardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        {
        }

        public PaymentMethod(int cardTypeId, string alias, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            _cardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new OrderDomainException(nameof(cardNumber));
            _securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new OrderDomainException(nameof(securityNumber));
            _cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new OrderDomainException(nameof(cardHolderName));

            if (expiration < DateTime.UtcNow)
            {
                throw new OrderDomainException(nameof(expiration));
            }

            _alias = alias;
            _expiration = expiration;
            _cardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return _cardTypeId == cardTypeId
                && _cardNumber == cardNumber
                && _expiration == expiration;
        }
    }
}