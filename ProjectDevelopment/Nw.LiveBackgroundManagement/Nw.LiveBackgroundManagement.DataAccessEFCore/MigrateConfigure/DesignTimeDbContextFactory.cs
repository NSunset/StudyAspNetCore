using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AuthorityDbContext>
    {
        public AuthorityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorityDbContext>();
            string connectionString = "server=192.168.157.128;database=LiveBackgroundManagement;uid=root;pwd=root;charset=utf8";
            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("5.7.37-mysql"));

            return new AuthorityDbContext(optionsBuilder.Options);
        }
    }
}
