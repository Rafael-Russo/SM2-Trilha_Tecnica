using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Account : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        //public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        //public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        public Account() : base()
        {
            Balance = 0;
        }
        public Account(string email) : base(email)
        {
            Balance = 0;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }
    }
}
