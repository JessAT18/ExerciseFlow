namespace fncConsumer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Odometer
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Datetime { get; set; }
        [Required]
        public long Steps { get; set; }
    }
}
    