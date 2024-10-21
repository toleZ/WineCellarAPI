using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Tasting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ICollection<Wine> Wines { get; set; } = new List<Wine>();

        [Required]
        public ICollection<string> Guests { get; set; } = new List<string>();
    }
}