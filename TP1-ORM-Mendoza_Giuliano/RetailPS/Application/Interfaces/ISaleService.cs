using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleService
    {
        Task<Sale> insertventa(Sale sale);
        Task<Sale> UpdateVenta(Sale sale);
        Task<List<Product>> GetAllProducts();
        Task<Sale> UpSaleandCalculate(Sale sale, SaleProduct saleProduct);


    }
}
