using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebSystemMvc.Models;

namespace WebSystemMvc.Data
{
    public class WebSystemMvcContext : DbContext
    {
        public WebSystemMvcContext (DbContextOptions<WebSystemMvcContext> options)
            : base(options)
        {
        }

        public DbSet<WebSystemMvc.Models.Department> Department { get; set; }
    }
}
