using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using AMMA202409018.API.Models.EN;


namespace AMMA202409018.API.Models.DAL
{
    public class AMMACRMContext : DbContext
    {
        public AMMACRMContext(DbContextOptions<AMMACRMContext> options) : base(options) { }

        public DbSet<ProductAMMA> Products { get; set; }
    }
}
