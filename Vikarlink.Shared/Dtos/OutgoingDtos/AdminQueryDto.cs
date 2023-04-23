using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vikarlink.Shared.Dtos.OutgoingDtos
{
    public class AdminQueryDto
    {
        public int Id { get; set; }
        public string ForNavn { get; set; } = string.Empty;
        public string EfterNavn { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrugerNavn { get; set; } = string.Empty;
        public string AdgangsKode { get; set; } = string.Empty;
        public DateTime DatoOprettet { get; set; } = DateTime.Now;
        public string? FotoURL { get; set; }
    }
}
