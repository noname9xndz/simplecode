using CleanArchitecture.Test.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Test.Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using CleanArchitecture.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Test.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
