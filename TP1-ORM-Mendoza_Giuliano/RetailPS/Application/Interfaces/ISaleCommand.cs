using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISaleCommand
    {
        Task<Sale> InsertVenta(Sale sale);
        Task<Sale> UpdateVenta(Sale sale);
    }
}
