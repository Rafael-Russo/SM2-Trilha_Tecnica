using ControleFinanceiro.Data;
using ControleFinanceiro.Models;
using ControleFinanceiro.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ControleFinanceiro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly ControleFinanceiroDbContext _context;
        private DateTime currentDate = DateTime.Now;

        public HomeController(ILogger<HomeController> logger, UserManager<Account> userManager, SignInManager<Account> signInManager, ControleFinanceiroDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return _context.Expense != null ?
                          View(await _context.Expense
                          .Where(e => e.AccountId == new Guid(_userManager.GetUserId(User)) &&
                                      e.DueDate.Month == currentDate.Month &&
                                      e.DueDate.Year == currentDate.Year
                          )
                          .OrderBy(e => e.DueDate)
                          .ToListAsync()) :
                          Problem("Entity set 'ControleFinanceiroDbContext.Expense'  is null.");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}