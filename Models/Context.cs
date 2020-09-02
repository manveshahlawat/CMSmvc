using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyCMS.Models
{
    public class Context:DbContext
    {
        public Context() : base("cs") { }

        public DbSet<Customer> customers { get; set; }

        public DbSet<Vendor> vendors { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<Orders> orders { get; set; }
    }
}