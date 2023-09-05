using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSystemMvc.Data;
using WebSystemMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace WebSystemMvc.Services
{
    public class DepartmentService
    {
        private readonly WebSystemMvcContext _context;

        public DepartmentService(WebSystemMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
