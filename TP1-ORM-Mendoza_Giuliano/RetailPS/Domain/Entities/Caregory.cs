using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Caregory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        //Relacion con la navegacion
        public ICollection<Product> Products { get; set; }

    }
}
