using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMMA202409018.DTOs.ProductsDTOs
{
    public class SearchQueryProductAMMADTO
    {
        [Display(Name = "Nombre")]
        public string? NombreAMMA_Like { get; set; }

        [Display(Name = "Página")]
        public int Skip { get; set; }

        [Display(Name = "CantReg X Página")]
        public int Take { get; set; }

        /// <summary>
        /// 1 = No se cuenta los resultados de la búsqueda
        /// 2 = Cuenta los resultados de la búsqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}