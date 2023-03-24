using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Project.Models
{
    public class VenderContext : DbContext
    {
        public DbSet<Vender> venders { get; set; }
    }
}