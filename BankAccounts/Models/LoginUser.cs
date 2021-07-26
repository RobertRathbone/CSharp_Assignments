using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    [NotMapped] // don't add table to db
    public class LoginUser
    {
        [Required(ErrorMessage = "need it to log in")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "pretty please.")]
        [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}