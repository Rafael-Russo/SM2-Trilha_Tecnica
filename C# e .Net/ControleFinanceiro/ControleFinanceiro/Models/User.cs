using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType (DataType.Currency)]
        public double Balance { get; set; }

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        public User()
        {
        }
        public User(string name, string email, string password, double balance)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Balance = balance;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }
    }
}
