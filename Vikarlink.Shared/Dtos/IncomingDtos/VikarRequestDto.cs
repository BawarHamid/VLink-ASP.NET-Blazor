using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vikarlink.Shared.Dtos.IncomingDtos
{
    public class VikarRequestDto : RegisterRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = "Fornavnet skal indeholde mere end 2 bogstaver")]
        public string ForNavn { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Efternavnet skal indeholde mere end 2 bogstaver")]
        public string EfterNavn { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Adressen skal indeholde mere end 2 bogstaver")]
        public string Adresse { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(8, ErrorMessage = "Telefonnummeret skal være 8-cifre...")]
        [MaxLength(8, ErrorMessage = "Telefonnummeret skal være 8-cifre...")]
        [Phone(ErrorMessage = "Indtast venligst et gyldigt telefonnummer")]
        public string Telefon { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Indtast venligst en gyldig e-mailadresse")]
        public string Email { get; set; } = string.Empty;
        
        public DateTime DatoOprettet { get; set; } = DateTime.Now;
        public DateTime? AnsaettelsesDato { get; set; } = DateTime.Now;
        public DateTime? OpsigelsesDato { get; set; }
        public string? FotoURL { get; set; }
    }
}
