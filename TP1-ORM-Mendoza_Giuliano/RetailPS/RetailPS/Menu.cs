using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infraestructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailPS
{
    public class Menu
    {

        SaleService _SaleServ;
        SaleProductService _SaleProductServ;
        ProductService _ProductServ;

        public Menu(SaleService SaleServ, SaleProductService SaleProductServ, ProductService productService)
        {
            _SaleServ = SaleServ;
            _SaleProductServ = SaleProductServ;
            _ProductServ = productService;
        }
        public async Task ShowMenu()
        {
            bool ConditionSale = true;
            while (ConditionSale)
            {
                Console.Clear();
                DisplayMainMenu();
                switch (Console.ReadLine())
                {
                    case "1":

                        await ListAllProducts();
                        break;
                    case "2":
                        await RegisterSale();
                        break;
                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("                                            Hasta Pronto!                                            ");
                        Console.ResetColor();
                        ConditionSale = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("                                     Opcion erronea, ingrese del 1 al 3 por favor");
                        Console.ResetColor();
                        break;
                }
            }
        }
        private void DisplayMainMenu()
        {
            Console.WriteLine("                                            Bienvenido al Menu de RetailPS                                             \n");
            Console.WriteLine("========================================================================================================================");
            Console.WriteLine("                             Para continuar seleccione una de las siguientes opciones:                                   ");
            Console.WriteLine("                                          1- Listar todos los productos                                              ");
            Console.WriteLine("                                          2- Registrar una venta                                              ");
            Console.WriteLine("                                          3- Salir                                              ");
            Console.WriteLine("========================================================================================================================");
            Console.Write("                                          Ingrese una opción: ");
        }
        private async Task ListAllProducts()
        {
                Console.Clear();
                int selectCategory = GetCategory();
                int entrada = 1;
                List<Product> listProducts = await _SaleServ.GetAllProducts();
                ShowProductC(listProducts, selectCategory, entrada);
                Console.Write("                                            Presione cualquier tecla para volver al menú...");
                Console.ReadKey(true);
        }
        private async Task RegisterSale()
        {
            Sale newSale = await _SaleServ.insertventa(new Sale());
            int saleId = newSale.SaleId;
            bool condition = true;
            while (condition)
            {
                Console.WriteLine("                                            Registrar Venta");
                Console.WriteLine("                                            Los productos estarán divididos por categoría\n");
                int selectCategory = GetCategory();
                int entrada = 2;
                List<Product> listProducts = await _SaleServ.GetAllProducts();
                bool validInput = false;
                Guid productId = Guid.Empty;
                int quantity = 0;
                do
                {
                    ShowProductC(listProducts, selectCategory, entrada);
                    Console.Write("\n                                           Ingrese el GUID del producto: ");
                    if (!Guid.TryParse(Console.ReadLine(), out productId))
                    {
                        HandleInvalidInput("                                           Formato inválido del GUID. Inténtelo de nuevo.");
                        continue;
                    }
                    validInput = true;
                } while (!validInput);
                validInput = false; 
                do
                {
                    Console.Write("                                           Ingrese la cantidad del producto que va a llevar: ");
                    if (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        HandleInvalidInput("                                           Formato inválido de un número entero. Inténtelo de nuevo.");
                        continue;
                    }
                    validInput = true;
                } while (!validInput);
                double price = await _ProductServ.GetPriceById(productId);
                int discount = await _ProductServ.GetDiscountById(productId);
                var saleProduct = await _SaleProductServ.NewSaleProduct(saleId,productId,price,discount,quantity);
                newSale = await _SaleServ.UpSaleandCalculate(newSale, saleProduct);
                string input;
                do
                {
                    Console.Write("\n                                           ¿Desea seguir agregando productos? (S/N): ");
                    input = Console.ReadLine().ToLower();
                    if (input != "s" && input != "n")
                    {
                        Console.Clear();
                        Console.WriteLine("                             Opción inválida. Por favor, ingrese 'S' para continuar o 'N' para finalizar.");
                    }
                } while (input != "s" && input != "n");
                if (input == "n")
                {
                    condition = false;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                            Producto agregado correctamente\n");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                            Venta registrada correctamente\n");
            DetalleVenta(newSale);
            Console.ResetColor();
            Console.WriteLine("                                            Presione una tecla para volver al menú...");
            Console.ReadKey();
        }
        private void HandleInvalidInput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
        private void ShowProduct(Product product)
        {
            Console.WriteLine("\n");
            Console.WriteLine("                                           Detalles del Producto:                                            ");
            Console.WriteLine("==================================================================================================================");
            Console.WriteLine("                                           GUID:" + product.ProductId + "                                      \n ");
            Console.WriteLine("                                           Name: " + product.Name + "                                      ");
            WriteLineFrac(product.Description, 50);
            Console.WriteLine("                                           Price: " + "$" + product.Price + "                                      \n");
            Console.WriteLine("                                           Category: " + product.Categoria.CategoryId + "                                           \n");
            Console.WriteLine("                                           Discount: " + product.Discount + "%" + "                                          \n");
            Console.WriteLine("                                           ImageUrl: " + product.ImageUrl + "                                           \n");
            Console.WriteLine("==================================================================================================================");
        }
        private int GetCategory()
        {
            int selectcategory;
            bool isValidInput;
            do
            {
                Submenu();
                Console.Write("                                            Ingrese una opción: ");
                string? input = Console.ReadLine();
                isValidInput = int.TryParse(input, out selectcategory);
                Console.Clear();
                if (!isValidInput || selectcategory < 1 || selectcategory > 10)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                                         ¡¡¡Debe indicar una categoria!!!                                           ");
                    Console.ResetColor();
                }
            } while (!isValidInput || selectcategory < 1 || selectcategory > 10);
            return selectcategory;
        }
        private void Submenu()
        {
            Console.WriteLine("                                            Seleccione una categoría                                             \n");
            Console.WriteLine("==================================================================================================================");
            Console.WriteLine("                                            1- Electrodomésticos                                             ");
            Console.WriteLine("                                            2- Tecnología y Electrónica                                             ");
            Console.WriteLine("                                            3- Moda y Accesorios                                             ");
            Console.WriteLine("                                            4- Hogar y Decoración                                             ");
            Console.WriteLine("                                            5- Salud y Belleza                                             ");
            Console.WriteLine("                                            6- Deportes y Ocio                                             ");
            Console.WriteLine("                                            7- Juguetes y Juegos                                             ");
            Console.WriteLine("                                            8- Alimentos y Bebidas                                             ");
            Console.WriteLine("                                            9- Libros y Material Educativo                                             ");
            Console.WriteLine("                                            10- Jardinería y Bricolaje                                             ");
            Console.WriteLine("==================================================================================================================");
        }
        private void ShowProductC(List<Product> ListProducts, int Scategory, int entrada)
        {
            foreach (Product product in ListProducts)
            {
                if (product.Categoria.CategoryId == Scategory)
                {
                    if (entrada == 1)
                    {
                        ShowProduct(product);
                    }
                    else if (entrada == 2)
                    {
                        ShowProductID(product);
                    }
                }
            }
        }
        private void ShowProductID(Product product)
        {
            Console.WriteLine("\n");
            Console.WriteLine("                                                         Productos: ");
            Console.WriteLine("======================================================================================================================");
            Console.WriteLine("                                            GUID:" + product.ProductId + "                                        ");
            Console.WriteLine("                                            Name: " + product.Name + "                                            ");
            Console.WriteLine("======================================================================================================================");
        }
        private static void WriteLineFrac(string text, int maxLength)
        {
            Console.WriteLine("                                           Description: ");
            for (int i = 0; i < text.Length; i += maxLength)
            {
                Console.WriteLine("                                           " + text.Substring(i, Math.Min(maxLength, text.Length - i)));
            }
        }
        private void DetalleVenta(Sale sale)
        {
            Console.WriteLine("                                            Resumen de la Venta:");
            Console.WriteLine($"                                            Subtotal: {sale.Subtotal.ToString("N2")}");
            Console.WriteLine($"                                            TotalDiscount: {sale.TotalDiscount.ToString("N2")}");
            Console.WriteLine($"                                            Taxes: {sale.Taxes}");
            Console.WriteLine($"                                            TotalPay: {sale.TotalPay.ToString("N2")}");
            Console.WriteLine($"                                            Date: {sale.Date}\n");

        }        
    }                      
}
