using App.MvcWebUI.Entities;
using System.ComponentModel.DataAnnotations;

namespace App.MvcWebUI
{
    public class RegisterNewUserViewModel
    {
        public List<CustomIdentityRole> Roles { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string SelectedRole { get; set; }
    }
}