using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data
{
    public class CategoryData : IEntityTypeConfiguration<Caregory>
    {
        public void Configure (EntityTypeBuilder<Caregory> builder)
        {
            builder.HasData(
                new Caregory { CategoryId = 1, Name= "Electrodomésticos"},
                new Caregory { CategoryId = 2, Name= "Tecnología y Electrónica"},
                new Caregory { CategoryId = 3, Name= "Moda y Accesorios"},
                new Caregory { CategoryId = 4, Name = "Hogar y Decoración"},
                new Caregory { CategoryId = 5, Name = "Salud y Belleza"},
                new Caregory { CategoryId = 6, Name = "Deportes y Ocio"},
                new Caregory { CategoryId = 7, Name = "Juguetes y Juegos"},
                new Caregory { CategoryId = 8, Name = "Alimentos y Bebidas" },
                new Caregory { CategoryId = 9, Name = "Libros y Material Educativo"},
                new Caregory { CategoryId = 10, Name = "Jardinería y Bricolaje"}
                );
        }
    }
}
