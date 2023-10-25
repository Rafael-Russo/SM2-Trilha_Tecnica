using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models.ViewModels
{
    [Keyless]
    public class RegisterViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} required")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} required")]
        public string Password { get; set; }

        [Display(Name = "Saldo")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Balance { get; set; }
    }
}
