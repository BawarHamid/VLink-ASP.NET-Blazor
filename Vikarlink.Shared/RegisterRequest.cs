using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vikarlink.Shared
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Brugernavnet er for lang (Maksimum 20 tegn")]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 15, MinimumLength = 8, ErrorMessage = "Adgangskoden er for lang (Maksimum 15 tegn")]
        public string Password { get; set; }
    }
}
