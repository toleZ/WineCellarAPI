using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CreateUserDTO
    {
        public string Username { get; set; }
        [MinLength(10), MaxLength(50)]
        public string Password { get; set; }
    }
}