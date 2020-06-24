using System;
using Microsoft.AspNetCore.WebUtilities;
using Pagination.WebApi.Pagination.Models;

namespace Pagination.WebApi.Pagination.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPageUri(PaginationModel filter ,string route)
        {
            var enpointUri = new Uri(string.Concat(_baseUri ,route));
            var modifiedUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
