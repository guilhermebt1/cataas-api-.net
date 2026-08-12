using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CataasApi.Models
{
    [Table("SearchHistory")]
    public class SearchHistory
    {
        [Key]
        public int Id { get; set;  }
        [Required]
        public required string SearchContent { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public int QuantidadeResultados { get; set; }
    }
}
