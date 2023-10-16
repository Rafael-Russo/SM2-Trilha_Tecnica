namespace ControleFinanceiro.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
