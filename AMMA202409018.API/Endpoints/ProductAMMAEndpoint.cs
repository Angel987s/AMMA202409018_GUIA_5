using AMMA202409018.API.Models.DAL;
using AMMA202409018.API.Models.EN;
using AMMA202409018.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Mvc;
using static AMMA202409018.DTOs.ProductsDTOs.SearchResultProductAMMADTO;

namespace AMMA202409018.API.Endpoints
{
    public static class ProductEndpoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {

            app.MapPost("/product/search", async (SearchQueryProductAMMADTO productDTO, ProductAMMADAL productDAL) =>
            {
                // Ajuste temporal para verificar si la búsqueda encuentra productos
                var product = new ProductAMMA
                {
                    NombreAMMA = productDTO.NombreAMMA_Like
                };

                // Omitir el conteo de filas inicialmente para simplificar la lógica
                var products = await productDAL.Search(product, skip: productDTO.Skip, take: productDTO.Take);

                // Verificar si se encontraron productos
                if (products == null || !products.Any())
                {
                    return Results.NotFound("No se encontraron productos con los criterios de búsqueda proporcionados.");
                }

                // Crear el resultado
                var productResult = new SearchResultProductAMMADTO
                {
                    Data = products.Select(s => new SearchResultProductAMMADTO.ProductAMMADTO
                    {
                        Id = s.Id,
                        NombreAMMA = s.NombreAMMA,
                        DescripcionAMMA = s.DescripcionAMMA,
                        PrecioAMMA = s.PrecioAMMA
                    }).ToList(),
                    CountRow = productDTO.SendRowCount == 2 ? await productDAL.CountSearch(product) : products.Count
                };

                return Results.Ok(productResult);
            });


            app.MapGet("/product/{id}", async (int id, ProductAMMADAL productDAL) =>
            {
                var product = await productDAL.GetById(id);

                if (product == null)
                {
                    return Results.NotFound("Producto no encontrado");
                }

                var productDTO = new GetIdResultProductAMMADTO
                {
                    Id = product.Id,
                    NombreAMMA = product.NombreAMMA,
                    DescripcionAMMA = product.DescripcionAMMA,
                    PrecioAMMA = product.PrecioAMMA
                };
                return Results.Ok(productDTO);
            });

            app.MapPost("/product", async (CreateProductAMMADTO productDTO, ProductAMMADAL productDAL) =>
            {
                var product = new ProductAMMA
                {
                    NombreAMMA = productDTO.NombreAMMA,
                    DescripcionAMMA = productDTO.DescripcionAMMA,
                    PrecioAMMA = productDTO.PrecioAMMA
                };

                int result = await productDAL.Create(product);

                if (result > 0)
                {
                    return Results.Created($"/product/{result}", new { Id = result });
                }
                return Results.StatusCode(500);
            });

            app.MapPut("/product", async (EditProductAMMADTO productDTO, ProductAMMADAL productDAL) =>
            {
                var product = new ProductAMMA
                {
                    Id = productDTO.Id,
                    NombreAMMA = productDTO.NombreAMMA,
                    DescripcionAMMA = productDTO.DescripcionAMMA,
                    PrecioAMMA = productDTO.PrecioAMMA
                };

                int result = await productDAL.Edit(product);

                if (result > 0)
                {
                    return Results.Ok(result);
                }
                return Results.StatusCode(500);
            });

            app.MapDelete("/product/{id}", async (int id, ProductAMMADAL productDAL) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest("ID inválido");
                }

                int result = await productDAL.Delete(id);

                if (result > 0)
                {
                    return Results.Ok("Producto eliminado exitosamente");
                }

                return Results.StatusCode(500);
            });
        }
    }
}