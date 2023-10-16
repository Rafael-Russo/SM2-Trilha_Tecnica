using ControleFinanceiro.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }
        public double Cost { get; set; }
        public ExpenseType Type { get; set; }
        public ExpenseStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Expense()
        {
        }
        public Expense(string description, DateTime dueDate, double cost, ExpenseType type, ExpenseStatus status, User user)
        {
            this.Description = description;
            this.DueDate = dueDate;
            this.Cost = cost;
            this.Type = type;
            this.Status = status;
            this.User = user;
        }
    }
}
