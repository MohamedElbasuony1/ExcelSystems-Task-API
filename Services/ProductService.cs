using AutoMapper;
using Contracts;
using DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,
                              IUserRepository userRepository,
                              IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProduct(int userid)
        {
            ICollection<Product> Query =await _productRepository.FindAllAsync(a => a.SupplierId == userid
                                                                             && a.IsDeleted == false);
            List<ProductModel> products = new List<ProductModel>();
            foreach (Product item in Query)
            {
                products.Add(_mapper.Map<ProductModel>(item));
            }
            return products;
        }

        public async Task<ProductModel> AddProduct(ProductModel productmodel,int supplierid)
        {
            Product product=_mapper.Map<Product>(productmodel);
            product.SupplierId = supplierid;
            return _mapper.Map<ProductModel>(await _productRepository.AddAsync(product));
        }

        public async Task UpdateProduct(ProductModel productmodel, int supplierid)
        {
            Product product = _mapper.Map<Product>(productmodel);
            product.SupplierId = supplierid;
            await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> IsExistByID(int productid)
        {
            return await _productRepository.ExistAsync(a => a.ID == productid);
        }

        public async Task DeleteProduct(Product product)
        {
            product.IsDeleted = true;
            await _productRepository.UpdateAsync(product);
        }

        public async Task<Product> GetProductById(int ID)
        {
           return await _productRepository.GetByIdAsync(ID);
        }
    }
}
