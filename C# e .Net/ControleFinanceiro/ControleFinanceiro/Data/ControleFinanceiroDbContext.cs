using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroDbContext : IdentityDbContext
    {
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }
        public DbSet<Expense>? Expense { get; set; }
    }
}
