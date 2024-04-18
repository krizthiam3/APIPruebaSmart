using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ProductCreateDto
    {       
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero mazximo de caracteres es de 60")]
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

    }
}
