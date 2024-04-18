using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UserLoginResponseDto
    {
        public User User { get; set; }
        public string? Token { get; set; }
    }
}