using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models.ViewModels
{
    [Keyless]
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} required")]
        public string Password { get; set; }
    }
}
