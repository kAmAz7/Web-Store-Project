using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB
{
    class StoreContextDB : DbContext
    {
        public StoreContextDB() 
            : base("name=StoreContextDb")
        {
          
        }
        public DbSet<User> UserTable { get; set; }
        public DbSet<Product> ProductTable { get; set; }
    }
}
