using ControleFinanceiro.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0} required")]
        public string Description { get; set; }

        [Display(Name = "Data de Vencimento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "{0} required")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Value { get; set; }

        [Display(Name = "Tipo de despesa")]
        [Required(ErrorMessage = "{0} required")]
        public ExpenseType Type { get; set; }

        [Display(Name = "Status da despesa")]
        [Required(ErrorMessage = "{0} required")]
        public ExpenseStatus Status { get; set; }
        public Guid AccountId { get; set; }

        public Expense() { }
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
