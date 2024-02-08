using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace MvcEFApp.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 20 charcters")]
        public string Name { get; set; } = string.Empty;
        [Required]

        public string City { get; set; }= string.Empty;
        [Required]
        public DateTime DateofBirth { get; set; }
        [Column(TypeName = "numeric(18,0)")]
        public decimal PhoneNumber { get; set; }
    }
}
