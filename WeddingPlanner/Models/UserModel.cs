using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key] // denotes PK, not needed if named ModelNameId
        public int UserId { get; set; }

        [Required(ErrorMessage = "You have a name?")]
        [MinLength(2, ErrorMessage = "Please have a name longer than 2 letters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "so, just the first name?")]
        [MinLength(2, ErrorMessage = "more than 2")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "email, please.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ours is Password123, what's yours?")]
        [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        public string Password { get; set; }

        [NotMapped] // don't add to DB
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "doesn't match password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // // public int WeddingId { get; set; }
        // public Wedding wedding { get; set; }
        public List<WeddingAttendees> AttendedWeddings { get; set; }

        public List<Wedding> UserWeddings { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

    }
}