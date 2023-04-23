using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vikarlink.Shared.Dtos.IncomingDtos
{
    public class ElevCreateDto
    {
        public string CprNr { get; set; } = string.Empty;
        public string ForNavn { get; set; } = string.Empty;
        public string EfterNavn { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? Mobil { get; set; } = string.Empty;
        public string KontaktNr { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DatoOprettet { get; set; } = DateTime.Now;
        public string? FotoURL { get; set; }
        public int? KlasseVaerelseId { get; set; }

    }
}
