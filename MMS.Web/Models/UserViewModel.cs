using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MMS.Data.Models;

namespace MMS.Web.Models
{
    public class UserViewModel
    {  

        [Required]
        public string Name { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage="PasswordConfirm and ConfirmPassword not save")]
        public string PasswordConfirm  { get; set; }
        
        [Required]
        public Role Role { get; set; }

    }
}