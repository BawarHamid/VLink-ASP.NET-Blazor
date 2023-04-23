namespace Vikarlink.ApiInterface.Models
{
    public class Admin : BaseEntity
    {
        
        public string ForNavn { get; set; } = string.Empty;
        public string EfterNavn { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrugerNavn { get; set; } = string.Empty;
        public string AdgangsKode { get; set; } = string.Empty;
        public DateTime DatoOprettet { get; set; }
        public string? FotoURL { get; set; }
    }
}
