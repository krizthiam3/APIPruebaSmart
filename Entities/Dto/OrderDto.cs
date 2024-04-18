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
    public class OrderDto
    {       
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(60, ErrorMessage = "El numero mazximo de caracteres es de 60")]
        public string OrderCode { get; set; }
        public DateTime RequiedDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Comments { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }


    }
}
