using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SaleProductService : ISaleProductService
    {
        private readonly ISaleProductCommand _command;

        public SaleProductService(ISaleProductCommand command)
        {
            _command = command;
        }
        public async Task<SaleProduct> RegisterSP(SaleProduct saleProduct) 
        {
           return await _command.insertSP(saleProduct);
        
        }
        public async Task<SaleProduct> NewSaleProduct(int saleId, Guid productId, double price, int discount, int quantity)
        {
            var saleProduct = new SaleProduct
            {
                Sale = saleId,
                Product = productId,
                Price = (decimal)price,
                Discount = discount,
                Quantity = quantity,
            };
            return saleProduct;
        }

    }
}
