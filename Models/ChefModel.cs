using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ChefsandDishes2.Models
{
    public class Chef
    {
        public int ChefId { get; set; }

    
    [Required(ErrorMessage = "is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "is required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "is required")]
    [Display(Name = "Date of Birth")]
    [BirthdayCheck]
    public DateTime DOB { get; set; }
    // public int DishID { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> Dishes { get; set; }
    public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
    public class BirthdayCheckAttribute : ValidationAttribute
    {

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime DOB = (DateTime)value;
        if (DOB > DateTime.Today)
            return new ValidationResult("Please Enter a Birthday before today.");
        return ValidationResult.Success;
    }
}
}