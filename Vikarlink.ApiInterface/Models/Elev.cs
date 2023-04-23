namespace Vikarlink.ApiInterface.Models
{
    public class Elev : BaseEntity
    {
        public string CprNr { get; set; } = string.Empty;
        public string ForNavn { get; set; } = string.Empty;
        public string EfterNavn { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? Mobil { get; set; } = string.Empty;
        public string KontaktNr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DatoOprettet { get; set; }
        public string? FotoURL { get; set; }

        public int? KlasseVaerelseId { get; set; }

        public KlasseVaerelse KlasseVaerelse { get; set; }  
    }
}
