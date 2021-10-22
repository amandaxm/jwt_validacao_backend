using jwt_second_version.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt_second_version.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> op) : base(op) { }
        public DbSet<Products> Product {get;set;}
        public DbSet<UserInfo> Users {get;set;}
    }
}
