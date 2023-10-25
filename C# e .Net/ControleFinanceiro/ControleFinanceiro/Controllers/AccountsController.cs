﻿using System;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;

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
                var user = new Account() { Name = model.Name, UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro de Registro. Campos não seguem o padrão!");
                    return View(model);
                }
            }
            ModelState.AddModelError(string.Empty, "Erro. Favor contatar um administrador!");
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
            try
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
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro de login. Favor contatar um administrador!");
                return View(model);
            }
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
        public async Task<IActionResult> Details(Guid? id)
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
        public async Task<IActionResult> Edit(Guid? id)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Balance,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Account account)
        {
            if (!id.Equals(account.Id))
            {
                Console.WriteLine("------------ Id não é igual!");
                return NotFound();
            }

            try
            {
                try
                {
                    Console.WriteLine(_context.Update(account));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        Console.WriteLine("------------ Conta não existe!");
                        return NotFound();
                    }
                    else
                    {
                        Console.WriteLine("------------ throw!");
                        throw;
                    }
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro: " + ex);
                Console.WriteLine("Erro: " + ex);
                return View(account);
            }
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
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
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private bool AccountExists(Guid id)
        {
            return (_context.Account?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }
    }
}
