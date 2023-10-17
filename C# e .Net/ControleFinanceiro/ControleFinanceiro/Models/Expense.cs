using ControleFinanceiro.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public ExpenseType Type { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public ExpenseStatus Status { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public int UserId { get; set; }

        public User User { get; set; }

        public Expense()
        {
        }
        public Expense(string description, DateTime dueDate, double cost, ExpenseType type, ExpenseStatus status,int userId)
        {
            this.Description = description;
            this.DueDate = dueDate;
            this.Cost = cost;
            this.Type = type;
            this.Status = status;
            this.UserId = userId;
        }
    }
}
