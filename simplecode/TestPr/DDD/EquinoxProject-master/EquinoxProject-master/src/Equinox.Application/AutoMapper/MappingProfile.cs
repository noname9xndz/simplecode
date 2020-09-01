using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Equinox.Application.ViewModels;
using Equinox.Domain.Commands.Customer;
using Equinox.Domain.Commands.Product;
using Equinox.Domain.Models;
using RegisterNewProductCommand = Equinox.Domain.Commands.Product.RegisterNewProductCommand;

namespace Equinox.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //DomainToViewModelMapping
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();

            //ViewModelToDomainMappingProfile
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate)).ReverseMap();
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate)).ReverseMap();

            CreateMap<ProductViewModel, RegisterNewProductCommand>()
                .ConstructUsing(c => new RegisterNewProductCommand(c.Name, c.Price)).ReverseMap();
            CreateMap<ProductViewModel, UpdateProductCommand>()
                .ConstructUsing(c => new UpdateProductCommand(c.Id, c.Name, c.Price)).ReverseMap();
        }
    }
}
