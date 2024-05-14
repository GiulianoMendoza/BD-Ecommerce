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
    public class SaleService : ISaleService
    {
        private readonly ISaleCommand _command;
        private readonly IProductService _productService;

        public SaleService(ISaleCommand command, IProductService productService)
        {
            _command = command;
            _productService = productService;
        }
        public async Task<Sale> insertventa(Sale sale)
        {
            sale.Date = DateTime.Now;
            sale.TotalPay = 0;
            sale.Subtotal = 0;
            sale.TotalDiscount = 0;
            sale.Taxes = 1.21m;
            return await _command.InsertVenta(sale); 
        }
        public async Task<Sale> UpdateVenta(Sale sale)
        {
            return await _command.UpdateVenta(sale);
        }
      
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> todosLosProductos = await _productService.GetAllProduct();
            return todosLosProductos;
        } 
        public async Task<Sale> UpSaleandCalculate(Sale sale, SaleProduct saleProduct)
        {
            decimal taxes = sale.Taxes;
            decimal subtotal = (decimal)saleProduct.Price * saleProduct.Quantity;
            decimal totalDiscount = ((decimal)saleProduct.Price * saleProduct.Discount / 100) * saleProduct.Quantity;
            decimal totalPay = (subtotal - totalDiscount) * taxes;
            sale.Subtotal += subtotal;
            sale.TotalDiscount += totalDiscount;
            sale.TotalPay += totalPay;
            await _command.UpdateVenta(sale);
            return sale;
        }

    }
}
