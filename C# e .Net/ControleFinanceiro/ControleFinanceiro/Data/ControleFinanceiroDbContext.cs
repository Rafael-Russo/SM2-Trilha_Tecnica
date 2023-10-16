using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroDbContext : DbContext
    {
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }
        public DbSet<ControleFinanceiro.Models.User>? User { get; set; }
        public DbSet<ControleFinanceiro.Models.Expense>? Expense { get; set; }
    }
}
