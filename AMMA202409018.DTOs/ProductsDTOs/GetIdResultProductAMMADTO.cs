using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMA202409018.DTOs.ProductsDTOs
{
    public class GetIdResultProductAMMADTO
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string NombreAMMA { get; set; }

        [Display(Name = "Descripción")]
        public string? DescripcionAMMA { get; set; }

        [Display(Name = "Precio")]
        public decimal PrecioAMMA { get; set; }
    }
}