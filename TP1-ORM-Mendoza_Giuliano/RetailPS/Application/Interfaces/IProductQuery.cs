using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductquery
    {
        public Task<List<Product>> GetAllProduct();
        public Task<double> GetPriceById(Guid ProductId);
        public Task<int> GetDiscountById(Guid ProductId);
    }
}
