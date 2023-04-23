using System.ComponentModel.DataAnnotations;
using Vikarlink.Shared;

namespace Vikarlink.ApiInterface.Models
{
    public class Vikar : BaseEntity 
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
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public DateTime DatoOprettet { get; set; }
        public DateTime? AnsaettelsesDato { get; set; }
        public DateTime? OpsigelsesDato { get; set; }
        public string? FotoURL { get; set; }
       
        public IEnumerable<Vagt> Vagt { get; set; }
    }
}
