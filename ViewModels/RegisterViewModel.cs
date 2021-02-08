using EmployeeMangment.Utlities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.ViewModels
{
    public class RegisterViewModel
    { 
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse",controller:"Account")]
        [ValidEmailDomian(allowDomain:"gmail.com",ErrorMessage ="Email Domain Must Be @gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirmation Password do not match")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }

    }
}
