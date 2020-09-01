using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.EventSourcedNormalizers.Customer;
using Equinox.Application.EventSourcedNormalizers.Product;
using Equinox.Application.ViewModels;
using FluentValidation.Results;

namespace Equinox.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
        
        Task<ValidationResult> Register(ProductViewModel customerViewModel);
        Task<ValidationResult> Update(ProductViewModel customerViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<ProductHistoryData>> GetAllHistory(Guid id);
    }
}
