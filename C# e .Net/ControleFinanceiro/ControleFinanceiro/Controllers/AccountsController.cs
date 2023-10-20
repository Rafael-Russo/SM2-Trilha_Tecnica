using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using Microsoft.AspNetCore.Authorization;
using ControleFinanceiro.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Xml.Linq;

namespace ControleFinanceiro.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ControleFinanceiroDbContext _context;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;


        public AccountsController(ControleFinanceiroDbContext context, UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return _context.Account != null ?
                        View(await _context.Account.ToListAsync()) :
                        Problem("Entity set 'ControleFinanceiroDbContext.Account'  is null.");
        }

        // GET: Accounts/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Accounts/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Account(model.Name, model.Email) { Name = model.Name, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário ja existe!");
                    return View(model);
                }
            }
            ModelState.AddModelError(string.Empty, "Erro de Registro. Campos não seguem o padrão!");
            return View(model);
        }

        // GET: Accounts/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Accounts/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuário ou senha incorretos!");
                    return View(model);
                }
            }
            ModelState.AddModelError(string.Empty, "Erro de login. Usuário ou senha fora do padrão!");
            return View(model);
        }

        // POST: Accounts/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Balance")] Account account)
        {
            if (!id.Equals(account.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'ControleFinanceiroDbContext.Account'  is null.");
            }
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return (_context.Account?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
