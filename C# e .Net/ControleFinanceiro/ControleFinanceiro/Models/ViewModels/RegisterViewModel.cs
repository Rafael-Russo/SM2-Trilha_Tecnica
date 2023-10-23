using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Models.ViewModels
{
    [Keyless]
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
    }
}
