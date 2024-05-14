using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class ProductQuery : IProductquery 
    {
        private readonly AppDbContext _context;
        public ProductQuery(AppDbContext DBcontext) 
        { 
            this._context = DBcontext;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            List<Product> todosLosProductos = await _context.Product
                .Include(p => p.Categoria)
                .ToListAsync();
            return todosLosProductos;
        }
        public async Task<double> GetPriceById(Guid ProductId)
        {
            var price = await _context.Product
                .Where(p => p.ProductId == ProductId)
                .Select(p => p.Price)
                .FirstOrDefaultAsync();
            if (price == default(double))
            {
                throw new ExceptionNotFound("                                            Producto no encontrado                                            ");
            }
            return price;
        }
        public async Task<int> GetDiscountById(Guid ProductId)
        {
            var discount = await _context.Product
                .Where (p => p.ProductId == ProductId)
                .Select(p => p.Discount)
                .FirstOrDefaultAsync();
            if(discount == default(int))
            {
                return 0;
            }
            return discount;
        }
    }
}
