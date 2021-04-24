  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;

  namespace ChefsandDishes2.Models
  {
      public class Dish 
      {
        [Key] // the below prop is the primary key, [Key] is not needed if named with pattern: ModelNameId
        public int DishId { get; set; }
 
        [Required(ErrorMessage = "is required")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        [Display(Name = "Dish")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Who, I said, who made you?")]
        // public string Chef { get; set; }
        // [Required(ErrorMessage = "Is it paper? or chocolate?")]
        // [Range(1,5, ErrorMessage = "Between 1 and 5, please.")]
        public int Tastiness { get; set; }
        [Required(ErrorMessage = "All food has Calories")]
        [Range(1,50000, ErrorMessage = "More than 0.")]
        public int Calories { get; set; }
        [Required(ErrorMessage = "Describe please...")]
        public string Description { get; set; }
        public int ChefId { get; set; }
 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
      }
  }