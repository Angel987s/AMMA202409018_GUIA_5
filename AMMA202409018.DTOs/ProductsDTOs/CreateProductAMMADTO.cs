using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMA202409018.DTOs.ProductsDTOs
{
    public class CreateProductAMMADTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string NombreAMMA { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(255, ErrorMessage = "El campo Descripción no puede tener más de 255 caracteres.")]
        public string DescripcionAMMA { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un número positivo.")]
        public decimal PrecioAMMA { get; set; }
    }
}
