using AutoMapper;
using DTOs;
using Models;
using System.Collections.Generic;

namespace Helpers.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<ICollection<Product>, IEnumerable<ProductModel>>().ReverseMap();
        }
    }
}
