using AutoMapper;
using Domain.Core.DTOs;
using Domain.Core.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            
            // Ensure Id is null to let MongoDB generate a valid ObjectId
            product.Id = null;
            
            // Generate OperationId if not provided (it might come from request if we allowed it, but usually generated here)
            if (product.OperationId == Guid.Empty)
            {
                product.OperationId = Guid.NewGuid();
            }

            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateProductAsync(string id, ProductRequest productRequest)
        {
             var existing = await _productRepository.GetByIdAsync(id);
             if (existing != null)
             {
                 _mapper.Map(productRequest, existing);
                 // ensure id matches (though map shouldn't overwrite if not in DTO or config, safe to re-assert)
                 existing.Id = id; 
                 // OperationId should probably be preserved or updated depending on requirements. 
                 // If DTO has it, mapper updates it. If not, it stays. 
                 
                 await _productRepository.UpdateAsync(id, existing);
             }
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
