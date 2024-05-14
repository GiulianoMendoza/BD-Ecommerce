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

namespace Infraestructure.Comand
{
    public class SaleProductCommand : ISaleProductCommand
    {
        private readonly AppDbContext _context;
        public SaleProductCommand(AppDbContext context) 
        {
            this._context = context;
        }
        public async Task<SaleProduct> insertSP (SaleProduct saleProduct)
        {
            try
            {
                _context.Add(saleProduct);
                _context.SaveChanges();
                return saleProduct;
            }
            catch (DbUpdateException)
            {
                throw new ExceptionNotFound("                                            No se pudo registrar un SaleProduct                                            ");
            }
        }
    }
}
