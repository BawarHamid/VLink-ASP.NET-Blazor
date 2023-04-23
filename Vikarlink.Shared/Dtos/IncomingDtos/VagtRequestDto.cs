using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vikarlink.Shared.Dtos.IncomingDtos
{
    public class VagtRequestDto
    {
        public DateTime Dato { get; set; } = DateTime.Now;
        public DateTime StartTid { get; set; } = DateTime.Now;
        public DateTime SlutTid { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;

        [Required]
        public int? VikarId { get; set; }
        [Required]
        public int? KlasseVaerelseId { get; set; }  
    }
}
