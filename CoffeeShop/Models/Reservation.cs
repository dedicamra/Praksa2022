using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$")]
        public string Phone { get; set; }
        [Required]
        public int TotalPeople{ get; set; }
        [Required]
        public DateTime Date{ get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Time { get; set; }
    }
}
