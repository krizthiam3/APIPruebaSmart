using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CategoryCreateDto
    {       
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero mazximo de caracteres es de 60")]
        public string? Nombre { get; set; }
        public bool Status { get; set; }
    }
}
