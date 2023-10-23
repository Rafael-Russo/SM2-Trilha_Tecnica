using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;

namespace ControleFinanceiro.Data
{
    public class ControleFinanceiroDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid >
    {
        public ControleFinanceiroDbContext(DbContextOptions<ControleFinanceiroDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("notdbo");

            modelBuilder.Entity<Account>(b =>
            {
                b.ToTable("Account");
                b.Property(e => e.Email).HasColumnName("Email");
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();
                //// Each User can have many UserLogins
                //b.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                //// Each User can have many entries in the UserRole join table
                //b.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur => ur.UserId)
                //    .IsRequired();
            });
        }
        public DbSet<Expense>? Expense { get; set; }
        public DbSet<Account>? Account { get; set; }
        public DbSet<RegisterViewModel>? RegisterViewModel { get; set; }
        public DbSet<LoginViewModel>? LoginViewModel { get; set; }
    }
}
