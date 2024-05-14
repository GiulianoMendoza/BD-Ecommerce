using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class SaleProductQuery : ISaleProductQuery
    {
        private readonly AppDbContext _context;
        public SaleProductQuery(AppDbContext DBcontext)
        {
            this._context = DBcontext;
        }
        public async Task<SaleProduct> GetSaleProduct(int ShoppingCartId)
        {
            return _context.SaleProduct.SingleOrDefault(sp => sp.ShoppingCartId == ShoppingCartId);
        }
    }
}
