using System;
using System.Collections.Generic;
using Pagination.WebApi.Pagination.Models;
using Pagination.WebApi.Pagination.Services;

namespace Pagination.WebApi.Pagination.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponseModel<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationModel validFilter,int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponseModel<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages)); 
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationModel(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationModel(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationModel(1, validFilter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationModel(roundedTotalPages, validFilter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
