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

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
    }
}
