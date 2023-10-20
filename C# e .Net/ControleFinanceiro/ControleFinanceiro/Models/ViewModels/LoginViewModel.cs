using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Models.ViewModels
{
    [Keyless]
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
