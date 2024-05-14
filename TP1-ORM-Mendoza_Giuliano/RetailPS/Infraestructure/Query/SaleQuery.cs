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
    public class SaleQuery : ISaleQuery
    {
        private readonly AppDbContext _context;
        public SaleQuery(AppDbContext DBcontext)
        {
            this._context = DBcontext;
        }
        public async Task<Sale> GetSale(int SaleId)
        {
            return _context.Sale.SingleOrDefault(s => s.SaleId == SaleId);
        }
    }
}
