
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Wine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Variety Variety { get; set; }

        [CurrentYearOrEarlier]
        public int Year { get; set; } = DateTime.Now.Year;

        public string Region { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Stock { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        internal class CurrentYearOrEarlierAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is int year)
                {
                    if (year > DateTime.Now.Year)
                    {
                        return new ValidationResult("The year cannot be greater than the current year.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}
