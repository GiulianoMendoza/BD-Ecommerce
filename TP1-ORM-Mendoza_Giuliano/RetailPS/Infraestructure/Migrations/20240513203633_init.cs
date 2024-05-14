using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caregory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caregory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(38,17)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Caregory_Category",
                        column: x => x.Category,
                        principalTable: "Caregory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleProduct",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProduct", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Product_Product",
                        column: x => x.Product,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleProduct_Sale_Sale",
                        column: x => x.Sale,
                        principalTable: "Sale",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Caregory",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Electrodomésticos" },
                    { 2, "Tecnología y Electrónica" },
                    { 3, "Moda y Accesorios" },
                    { 4, "Hogar y Decoración" },
                    { 5, "Salud y Belleza" },
                    { 6, "Deportes y Ocio" },
                    { 7, "Juguetes y Juegos" },
                    { 8, "Alimentos y Bebidas" },
                    { 9, "Libros y Material Educativo" },
                    { 10, "Jardinería y Bricolaje" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Category", "Description", "Discount", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("03373629-8ca2-4af1-9401-63cc43c0f397"), 10, "Con la Motosierra Philco podés hacer cortes profesionales de todo tipo de maderas, para podar, cortar árboles y hacer numerosos trabajos de jardinería. Es tu aliada perfecta para superar cualquier obstáculo. Con su diseño ergonómico y potente motor, te brinda la confianza y el control necesarios para realizar trabajos eficientes y precisos", 0, "https://i.imgur.com/kuXUj62.jpeg", "Motosierra Philco A Nafta Espada 45 Cm 52cc ", 199.999m },
                    { new Guid("03b32429-219f-4521-8350-2fe4916a74ac"), 9, "Editorial: RBA Bolsillo, páginas: 160, idioma Español. Clasificación Ingenieria, Tecnica Y Ciencias Exactas - Ingenieria - General", 0, "https://i.imgur.com/25XVmpI.jpeg", "Inteligencia Artificial (bolsillo) - Ignasi Belda Reig", 15.9m },
                    { new Guid("04ed858a-c681-4ae4-ab53-999b0acbd419"), 6, "Este casco puede usarse tanto en jóvenes como adultos ya que es regulable de los 57CM a los 62CM. Es muy fácil de trasladar ya que es muy liviano y cómodo a la hora de usar y hacer viajes largos.", 23, "https://i.imgur.com/OvyMriJ.jpeg", "Casco Bicicleta Zykel Con Visera", 27.624m },
                    { new Guid("06e8d3fe-d0e2-4667-b7b3-0c2a733e1d08"), 8, "Quínoa, zanahoria deshidratada, cebolla deshidratada, morrón deshidratado, espinaca deshidratada, perejil deshidratado, sal, azúcar, pimentón dulce, nuez moscada, coriandro, pimienta blanca.", 0, "https://i.imgur.com/TpkqwZ0.jpeg", "Quinoa Con Vegetales Y Champignones", 5.85m },
                    { new Guid("0a08f1c6-5fce-4e41-a7e9-c33bcb46ee68"), 10, "Con su potencia de 105 bar podés dejar reluciente cualquier material o superficie. Su impacto de agua a alta presión es entre diez y cincuenta veces más potente que las mangueras de jardín, lo que hace más eficaz y fácil el proceso de limpieza.", 33, "https://i.imgur.com/L2rsHLN.jpeg", "Hidrolavadora Daewoo 1200 Watts 105 Bar con Autostop", 149.999m },
                    { new Guid("0b78972c-e3c8-4df2-a7a8-147325139e74"), 6, "Una bicicleta fija, equipada con un volante de inercia resistente, puede proporcionar Más potencia y te ofrece una experiencia deportiva desafiante", 0, "https://i.imgur.com/orXy11W.jpeg", "Bicicleta Fija Para Spinning Tm Acero", 272.599m },
                    { new Guid("0d810fa1-f6e5-4c07-9682-8b0a74defc73"), 7, "Monopoly Classic es un juego de mesa divertido y emocionante que es perfecto para familias y amigos. Es un juego de estrategia, negociación y suerte, y siempre hay algo nuevo que aprender.", 29, "https://i.imgur.com/Ov0H3lN.jpeg", "Juego de Mesa Monopoly Clásico", 99.99m },
                    { new Guid("0f8006dc-44bd-4e41-976f-db90f7af5861"), 4, "Botón táctil » Luz: 3000 K - 6000 K » 120 LED por metro (1360 lúmenes) » Función antiempañe » Función lupa (x5) » Reloj, fecha, temperatura » Alimentación 220 V » Transformador 12 V", 0, "https://i.imgur.com/xontgIp.jpg", "Espejos Grandes Redondo Pared Con Luz Led Moderno Tactil", 383.613m },
                    { new Guid("15850893-6c85-4f35-85dc-f6fa16252877"), 1, "La heladera Sigma combina su cómodo diseño con una gran utilidad. En su interior, cuenta con un bandejón con tapa y 3 prácticos estantes para que puedas acomodar tus alimentos de la mejor forma…", 0, "https://i.imgur.com/GkJkcHz.jpg", "Heladera Cíclica Sigma 239Lts", 559.999m },
                    { new Guid("25307730-5fe2-4676-8efc-92d6dff30efd"), 3, ":Una riñonera con estilo universitario, hecha parcialmente con materiales reciclados Más que una marca de nuestro legado, el monograma del Trifolio de esta canguro adidas es una declaración sutil que hace que cada outfit sea más prémium.", 10, "https://i.imgur.com/GNccHqp.jpeg", "Riñonera adidas Wb L Trefoil Aop", 58.999m },
                    { new Guid("275928b0-89f1-4549-9243-9d645ff4a2ed"), 9, "Que son las contraseñas? ?Como se construyen? ?En que momento de la historia surgieron? Estas son solo algunas de las preguntas que sirven como disparador para que Martin Paul Eve despliegue su conocimiento sobre las implicancias y el funcionamiento de las contraseñas", 0, "https://i.imgur.com/Hz41tOe.jpeg", "Una Historia De Las Contr45eñ4s - Martin Paul Eve ", 17.999m },
                    { new Guid("278af4c4-742c-4e96-9804-767629459eab"), 3, "Detalles: Acetato, Polarizado, Calibre 49, Filtro UV 100%.", 10, "https://i.imgur.com/Vj9Dy6c.jpg", "Anteojos Vulk Rolling Stones I Can T", 172.299m },
                    { new Guid("2b21831b-d545-4d80-b0d2-2a4f739e4cb4"), 5, "Remueve impurezas de la piel, efecto antiarrugas y antiojeras, reduce las arrugas de ojos y ojeras, promueve la absorción de la crema para la piel, alivia la fatiga de los ojos, 2 velocidades de vibración, funciona con pila CR2032 (incluida).", 35, "https://i.imgur.com/Zo8NGy8.jpg", "Balanza De Vidrio Para Baño Beurer Gs-10 ", 31.04m },
                    { new Guid("312cc244-12f8-451e-820b-a64630ef43cb"), 2, "Descubrí la excepcional ligereza y delgadez de este portátil, convirtiéndolo en un equipo versátil y fácil de llevar a todas partes. Su pantalla Full HD de 15.6 pulgadas no solo proporciona comodidad para trabajar, sino que también garantiza una calidad de imagen excepcional.", 0, "https://i.imgur.com/zfmWA1i.jpg", "Notebook HP 15 Core i3 8GB", 879.999m },
                    { new Guid("3dfd2a91-a604-4f96-9d59-3ee71147ecc2"), 9, "Explora la evolución de los videojuegos desde sus orígenes en el MIT hasta su posición actual como fenómeno mundial. Analiza la influencia de los arcades en la popularización del medio, el legado de Mario y su impacto en la cultura pop, y el éxito de Pokémon como estrategia de mercadeo basada en la nostalgia.", 0, "https://i.imgur.com/6Z2OS3v.jpeg", "FENOMENOS GAMERS, de Nicolas Rabago", 19.6m },
                    { new Guid("417fe660-911b-4449-be55-ea7ab82eda3b"), 4, "Con indicadores de confort, incluso pequeños cambios de temperatura, control de humedad en un vistazo. Supervisión continua de los cambios de temperatura y humedad en el interior. Atención y apoyo durante todo el año para la salud de su familia", 50, "https://i.imgur.com/X6ZHONh.jpg", "Reloj monitor de temperatura y humedad Xiaomi", 126.789m },
                    { new Guid("4c23c2a7-2eff-4e75-accd-b026d46425fd"), 8, "Agua carbonatada, azúcar, colorante, aromas (contiene cafeína), acidulante y edulcorantes. No contiene alérgenos de obligada declaración. Mejor que bueno.", 0, "https://i.imgur.com/BljOtPT.jpeg", "Gaseosa Pepsi Botella 2lt", 2.4m },
                    { new Guid("4f4883fc-eebd-4f9c-8b12-3957b24418ab"), 7, "Incluye 70 piezas, 2 muñecos, 15 stickers, guía de armado, edad recomendada 5 años, juguete no tóxico, fabricado sin ftalatos ni colorantes.", 22, "https://i.imgur.com/jLo3URK.jpeg", "Juego De Bloques Blocky Bomberos", 8.999m },
                    { new Guid("53c26ac4-6906-45e2-9847-2d4dfd38f910"), 8, "Nada supera el sabor de una Coca-Cola. Diseñado para acompañar cada momento, el sabor de la Coca-Cola es un clásico que perdura desde hace más de 130 años. Deliciosa y refrescante.", 0, "https://i.imgur.com/P3Ca0DP.jpeg", "Gaseosa Coca-cola Sabor Original 2.25 L", 2.175m },
                    { new Guid("646a8a2e-70d1-41a8-83ae-69ae067652e7"), 2, "Cuenta con una excelente pantalla de 27” pulgadas y resolución FHD (1920x1080) para que disfrutes de ver todo tu contenido en gran calidad.", 15, "https://i.imgur.com/z4xwwrR.jpg", "Monitor Noblex 27” FHD", 329.999m },
                    { new Guid("6cade72d-d993-44da-890b-c93982361d96"), 7, "Panel LED con teclas luminosas, colorida pantalla, entrada y salida de audio, control deslizante de volumen, efectos scratch, pulsadores para varios efectos, selección de ritmo, altavoz, auriculares.", 29, "https://i.imgur.com/kkMdvBA.jpeg", "Consola Dj Mixer Juguete con Luz y Sonido  ", 56.999m },
                    { new Guid("759c833c-0221-411f-b198-5a8bee6aee30"), 3, "Billetera de la marca Billabong, confeccionada con Cuero sintético, con monedero y porta tarjetas interior.", 0, "https://i.imgur.com/xontgIp.jpeg", "Billetera Billabong Dimension M Wllt Hombre", 29.999m },
                    { new Guid("78047171-d43b-443b-9628-e0a144eb5c74"), 6, "Este casco puede usarse para adultos, es talle L con un calce cómodo y regulable.", 0, "https://i.imgur.com/Df9qwaf.jpeg", "Casco Bicicleta Raleigh 29vent.", 48.889m },
                    { new Guid("7c944be6-b026-4996-a0a4-3ab5940ae5fc"), 10, "La cortadora de cesped eléctrica DAC1600 de Daewoo cuenta con una potencia de 1600 watts , mientras que, el voltaje es de 220/50 Hz. A su vez posee una manija ergonómica para una mayor comodidad del usuario, es plegable y pertenece a la línea ECO. ", 42, "https://i.imgur.com/mqeEhE0.jpeg", "Cortadora de césped Daewoo Eléctrica ", 349.999m },
                    { new Guid("855fed98-c34a-48f5-838e-f3ff723ea645"), 5, " Estructura: 80% madera maciza y 20% aglomerado de partículas.Asiento: Cinchas elásticas y placa poliéster soft. Relleno: Guata y copo de poliéster. Costura: Costura reforzada. Patas: Inyección plástica.", 51, "https://i.imgur.com/6m2ZjKx.jpeg", "Masajeador Facial Anti Arrugas y Ojeras", 29.999m },
                    { new Guid("866dc5bf-efda-4dcb-935c-f2de39837115"), 8, "Paquete en versión de 500g, Porotos alubias, puede contener soja", 0, "https://i.imgur.com/jSEf9QO.jpeg", "Porotos Mister Food Alubia", 550m },
                    { new Guid("9d4f1438-edb6-4516-9ab6-694e4aa34528"), 2, "Mediante su entrada PC In podrás conectar tu PC o Notebook. Además, también ofrece la posibilidad de conectarse a través de HDMI. El LED no tiene sistema de audio incorporado… ", 0, "https://i.imgur.com/eHBufmj.jpg", "Monitor Gamer Samsung 27”", 299.999m },
                    { new Guid("a6c33233-2e59-4a7c-9ec7-2188e1e942e1"), 9, "Editorial: Oberon, páginas: 336, idioma Español. Clasificación Ingenieria, Tecnica Y Ciencias Exactas - Otras Ciencias Exactas - Astronomia", 0, "https://i.imgur.com/kZgLnD3.jpeg", "Vida En Marte - David A. Weintraub", 23.56m },
                    { new Guid("a6cf2675-4a42-4e7b-bf3e-7898004eafb3"), 5, "adnic presenta su nueva linea de Mascara Led ! Esta mascara es ideal para el cuidado de la piel y combatir el acné. Gracias a su gran pantalla táctil hace que sea muy fácil a la hora de usar. Cuenta con 192 luces LED y 7 diferentes colores. Dependiendo del tipo de luz elegida se pueden realizar diferentes tratamientos.", 0, "https://i.imgur.com/bSnTN9d.jpeg", "Mascara Facial Led Gadnic LT3.0 Terapia Acné Rejuvenecimiento", 77.999m },
                    { new Guid("b0584d95-bb62-4044-b0f7-aad319606dff"), 6, "Cinta motorizada para caminar y correr. Su consola LED posee un diseño moderno con luz blanca y 4 ventanas que permiten monitorear velocidad, tiempo, distancia y calorías consumidas.", 25, "https://i.imgur.com/wxiH52I.jpeg", "Cinta Motorizada Randers ARG-472 ", 999.999m },
                    { new Guid("c717ec6b-7889-4193-a952-e81e236935a0"), 7, "Incluye 70 piezas, 2 muñecos, 15 stickers, guía de armado, edad recomendada 5 años, juguete no tóxico, fabricado sin ftalatos ni colorantes.", 0, "https://i.imgur.com/nCDdUY9.jpeg", "Juego De Bloques Blocky Policías", 8.999m },
                    { new Guid("cf324d5d-97b5-4471-ac42-a8b32abadf56"), 1, "La heladera cíclica Drean cuenta con un diseño clásico tipo Top Mount, es decir que ubica el freezer en la parte superior y en la parte inferior el refrigerador…", 28, "https://i.imgur.com/R8YiUCy.jpeg", "Heladera Cíclica Drean 277Lts", 815.799m },
                    { new Guid("e58f9896-7c35-4bd1-8af3-b4207e5c9080"), 4, "Estructura: 80% madera maciza y 20% aglomerado de partículas.Asiento: Cinchas elásticas y placa poliéster soft. Relleno: Guata y copo de poliéster. Costura: Costura reforzada. Patas: Inyección plástica.", 39, "https://i.imgur.com/JrI2KwS.jpg", "Sillón Esquinero Gris Piazza Chaise Longue G3", 985m },
                    { new Guid("ebd13ef0-7fb2-4368-b038-5ddbef6628a9"), 5, "Es una crema de una avanzada tecnología que no es grasosa y brinda una mayor potencia de humectación para pieles o zonas con mayor resequedad o sensibles", 20, "https://i.imgur.com/1dbYScK.jpeg", "Crema Hidratante Piel Extra Seca Y Sensible Cetaphil ", 29.19m },
                    { new Guid("f2e6eb9b-6d45-48b8-92db-361ae9c2dcd7"), 3, " Detalles: Plastico, Polarizado, Filtro UV 100%.", 0, "https://i.imgur.com/j9QGnA4.jpg", "Anteojos Vulk Evan C14 Hombre", 27.199m },
                    { new Guid("f4bcbc98-3a58-4a9f-9258-b67e56cc26ba"), 2, "La notebook lenovo IDEAPAD 1 es exactamente lo que necesita una computadora portátil de uso diario. Vea programas en una pantalla HD de hasta 14 pulgadas con un marco extremadamente delgado.", 39, "https://i.imgur.com/aC6Tpbp.jpg", "Notebook Lenovo Ideapad 1", 599.999m },
                    { new Guid("f699c2a3-4119-4ba5-8166-e6e6cf86fe02"), 4, "Diseño pensado a detalle para que desbloquees el siguiente nivel de confort en tu lugar reservado para jugar hasta que en la pantalla de tu adversario aparezca game over.", 0, "https://i.imgur.com/bHhg5Jk.jpg", "Escritorio Gamer PC 2M", 217.14m },
                    { new Guid("f7f70071-3f41-4f15-ac71-04d0e8766515"), 10, " La bordeadora de césped severbon cuenta con motor eléctrico y una potencia de 1000 watts. Posee una manija ajustable en altura, que te brindará una mayor comodidad y te facilitará su uso.", 40, "https://i.imgur.com/QeJly67.jpeg", "Bordeadora Eléctrica Severbon", 99.999m },
                    { new Guid("f813177c-0361-4ddb-b90a-3a1e9227d1c5"), 1, "La Escorial Candor S2 es una cocina de diseño clásico en color negro que cuenta con cuatro hornallas; horno y cajón parrilla independiente…", 9, "https://i.imgur.com/Enf28Zo.jpg", "Cocina Escorial Candor S2 Gas Natural", 273.099m },
                    { new Guid("f82fa483-4240-4c01-8b3a-d52f8799af0e"), 1, "La cocina electrolux 56DXQ cuenta con 2 hornos independientes, uno eléctrico y uno a gas, permitiendo que se puedan realizar preparaciones simultáneamente y con excelente calidad…", 10, "https://i.imgur.com/UiRaVyJ.jpg", "Cocina Electrolux 56DXQ ", 999.999m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Category",
                table: "Product",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Product",
                table: "SaleProduct",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_SaleProduct_Sale",
                table: "SaleProduct",
                column: "Sale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Caregory");
        }
    }
}
