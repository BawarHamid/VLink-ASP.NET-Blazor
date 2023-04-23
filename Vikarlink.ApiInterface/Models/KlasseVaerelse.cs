using System.ComponentModel.DataAnnotations;

namespace Vikarlink.ApiInterface.Models
{
    public class KlasseVaerelse : BaseEntity
    {
        //[Required]
        //public int Id { get; set; } 
        public string LokaleInfo { get; set; } = string.Empty;

        public IEnumerable<Vagt> Vagt { get; set; }
        public IEnumerable<Elev> Elev { get; set; }

    }
}
