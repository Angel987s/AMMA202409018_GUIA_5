using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMMA202409018.API.Models.EN
{
    public class ProductAMMA
    {
        
        public int Id { get; set; }
        public string NombreAMMA { get; set; }
        public string DescripcionAMMA { get; set; }
        public decimal PrecioAMMA { get; set; }
    }
}
