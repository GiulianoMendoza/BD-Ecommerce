using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleProductService
    {
        Task<SaleProduct> RegisterSP(SaleProduct saleProduct);
        Task<SaleProduct> NewSaleProduct(int saleId, Guid productId, double price, int discount, int quantity);
    }
}
