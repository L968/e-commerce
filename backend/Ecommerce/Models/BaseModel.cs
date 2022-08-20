using System.ComponentModel;

namespace Ecommerce.Models
{
    public class BaseModel
    {
        [Required]
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }

        [Required]
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
    }
}