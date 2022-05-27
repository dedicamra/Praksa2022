using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kurs2.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Titile { get; set; }
        [Required]
        [StringLength(30)]
        public string Author { get; set; }
        [Required]
        [StringLength(500)]
        public string Descripton { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime CreatedAt{ get; set; }

        [JsonIgnoreAttribute]
        public string UserId { get; set; }
    }
    
}
