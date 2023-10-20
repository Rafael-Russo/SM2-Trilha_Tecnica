using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.Models
{
    public class Account : IdentityUser
    {
        public override string Id { get { return base.Id; } }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get { return base.PasswordHash; } }
        public double Balance { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        public Account(string name, string email) : base(email)
        {
            Name = name;
            Balance = 0;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }
    }
}
