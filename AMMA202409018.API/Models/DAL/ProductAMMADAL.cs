using AMMA202409018.API.Models.EN;
using Microsoft.EntityFrameworkCore;

namespace AMMA202409018.API.Models.DAL
{
    public class ProductAMMADAL
    {
        readonly AMMACRMContext _context;  

        public ProductAMMADAL(AMMACRMContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductAMMA product)
        {
            _context.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductAMMA> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product != null ? product : new ProductAMMA();
        }

        public async Task<int> Edit(ProductAMMA product)
        {
            int result = 0;
            var productUpdate = await GetById(product.Id);
            if (productUpdate.Id != 0)
            {
                // Update product details
                productUpdate.NombreAMMA = product.NombreAMMA;
                productUpdate.DescripcionAMMA = product.DescripcionAMMA;
                productUpdate.PrecioAMMA = product.PrecioAMMA;
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var productDelete = await GetById(id);
            if (productDelete.Id > 0)
            {
                _context.Products.Remove(productDelete);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        private IQueryable<ProductAMMA> Query(ProductAMMA product)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(product.NombreAMMA))
            {
                query = query.Where(p => p.NombreAMMA.Contains(product.NombreAMMA));
            }

            return query;
        }

        public async Task<int> CountSearch(ProductAMMA product)
        {
            return await Query(product).CountAsync();
        }

        public async Task<List<ProductAMMA>> Search(ProductAMMA product, int take = 10, int skip = 0)
        {
            take = take > 10 ? 10 : take;
            var query = Query(product);
            query = query.OrderByDescending(p => p.Id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }
    }
}
