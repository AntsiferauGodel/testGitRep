using System.ComponentModel.DataAnnotations;
using Lab08.MVC.Data.RoleEnums;

namespace Lab08.MVC.Business.Models
{
    public class UserRegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Choose your destiny")]
        [Required]
        public Roles RoleEnum { get; set; }
    }
}
