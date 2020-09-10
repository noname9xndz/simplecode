namespace Basket.Infrastructure.Exception
{
    public class BasketDomainException : System.Exception
    {
        public BasketDomainException()
        { }

        public BasketDomainException(string message)
            : base(message)
        { }

        public BasketDomainException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}