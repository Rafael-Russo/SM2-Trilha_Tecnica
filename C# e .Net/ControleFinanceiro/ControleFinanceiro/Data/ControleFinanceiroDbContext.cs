using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroDbContext : IdentityDbContext<Account>
    {
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
            .Property(e => e.Email)
            .HasColumnType("email");

            builder.Entity<Account>()
                .Property(e => e.Password)
                .HasColumnType("password");
        }
        public DbSet<Expense>? Expense { get; set; }
        public DbSet<Account>? Account { get; set; }
        public DbSet<RegisterViewModel>? RegisterViewModel { get; set; }
        public DbSet<LoginViewModel>? LoginViewModel { get; set; }
    }
}
