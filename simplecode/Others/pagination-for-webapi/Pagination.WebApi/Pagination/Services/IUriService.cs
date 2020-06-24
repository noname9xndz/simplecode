using System;
using Pagination.WebApi.Pagination.Models;

namespace Pagination.WebApi.Pagination.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationModel filter, string route);
    }
}
