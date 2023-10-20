using ControleFinanceiro.Models.Enums;

namespace ControleFinanceiro.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public double Value { get; set; }
        public ExpenseType Type { get; set; }
        public ExpenseStatus Status { get; set; }
    }
}
