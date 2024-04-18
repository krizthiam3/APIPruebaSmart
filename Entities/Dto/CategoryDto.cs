using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CategoryDto
    {       
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero mazximo de caracteres es de 60")]
        public string? Nombre { get; set; }

    }
}
