using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Equinox.Application.EventSourcedNormalizers;
using Equinox.Application.EventSourcedNormalizers.Customer;
using Equinox.Application.EventSourcedNormalizers.Product;
using Equinox.Application.Interfaces;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands.Customer;
using Equinox.Domain.Commands.Product;
using Equinox.Domain.Core.Core.Mediator;
using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Repository.EventSourcing;
using FluentValidation.Results;

namespace Equinox.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler _mediator;

        public ProductAppService(IMapper mapper,
                                  IProductRepository productRepository,
                                  IMediatorHandler mediator,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ValidationResult> Register(ProductViewModel productViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewProductCommand>(productViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(ProductViewModel productViewModel)
        {
            var updateCommand = _mapper.Map<UpdateProductCommand>(productViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveProductCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IList<ProductHistoryData>> GetAllHistory(Guid id)
        {
            var aggregate = await _eventStoreRepository.All(id);
            return ProductHistory.ToJavaScriptProductHistory(aggregate);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
