using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Wedding{
        [Key]
        public int WeddingId { get; set; }

        [Required(ErrorMessage = "You have a name?")]
        [Display(Name = "The betrothed")]
        public string WedderOne { get; set; }
        [Required(ErrorMessage = "You have a name?")]
        [Display(Name = "The other betrothed")]
        public string WedderTwo { get; set; }
        [Required(ErrorMessage = "Pick a spot on the space time continuum... maybe June?")]
        [DataType(DataType.Date)]
        [FutureDate]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "I hear Jasper's Farm is nice")]
        
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User CreatedBy { get; set; }
        public List<WeddingAttendees> Attendees { get; set; }

        public class FutureDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime date = (DateTime)value;
                return date < DateTime.Now ? new ValidationResult("You can't plan your weddin yesterday.") : ValidationResult.Success;
            }
        }
    }
}