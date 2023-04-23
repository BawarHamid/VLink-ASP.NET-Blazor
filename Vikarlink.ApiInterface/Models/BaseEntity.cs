using System.ComponentModel.DataAnnotations;

namespace Vikarlink.ApiInterface.Models
{
    public abstract class BaseEntity
    {
        [Required]
        [Range(1, 500, ErrorMessage = "Id has to be between 1 & 1000")]
        public int Id { get; set; }
    }
}
