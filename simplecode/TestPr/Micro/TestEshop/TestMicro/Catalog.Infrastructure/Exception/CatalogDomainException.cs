namespace Catalog.Infrastructure.Exception
{
    /// <summary>
    /// Exception type for app exceptions
    /// </summary>
    public class CatalogDomainException : System.Exception
    {
        public CatalogDomainException()
        { }

        public CatalogDomainException(string message)
            : base(message)
        { }

        public CatalogDomainException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}