using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string? OrderCode { get; set; }
        public DateTime RequiedDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string? Comments { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }       
    }
}
