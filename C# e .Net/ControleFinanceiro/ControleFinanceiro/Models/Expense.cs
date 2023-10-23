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
        public Guid AccountId { get; set; }

        public Expense(string description, DateTime dueDate, double value, ExpenseType type, ExpenseStatus status, Guid accountId)
        {
            Description = description;
            DueDate = dueDate;
            Value = value;
            Type = type;
            Status = status;
            AccountId = accountId;
        }
    }
}
