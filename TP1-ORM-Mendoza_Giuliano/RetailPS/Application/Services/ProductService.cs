using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductquery _query;
        public ProductService(IProductquery Productquery)
        {
            _query = Productquery;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            List<Product> todosLosProductos = await _query.GetAllProduct();
            return todosLosProductos;
        }
        public async Task<double> GetPriceById(Guid ProductId)
        {
            return await _query.GetPriceById(ProductId);
        }
        public async Task<int> GetDiscountById(Guid ProductId)
        {
            return await _query.GetDiscountById(ProductId);
        }
    }
}
