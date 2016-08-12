using System;
using System.ComponentModel.DataAnnotations;

namespace PetesSalon.DomainClasses
{
   public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Role")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

    }
}
