namespace Vikarlink.ApiInterface.Models
{
    public class Vagt : BaseEntity
    {
        public DateTime Dato { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string Status { get; set; } = string.Empty;

        public int? VikarId { get; set; }
        public int? KlasseVaerelseId { get; set; }

        public Vikar Vikar { get; set; }
        public KlasseVaerelse KlasseVaerelse { get; set; }

    }
}
