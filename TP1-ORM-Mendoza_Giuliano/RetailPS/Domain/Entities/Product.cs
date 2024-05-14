using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Category { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { get; set; }

        //Relacion con la navegacion
        public ICollection<SaleProduct> SaleProducts { get; set; }
        public Caregory Categoria { get; set; }

    }
}
