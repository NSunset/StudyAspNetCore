using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using IdentityServer.CodeMode.Persistence.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.CodeMode.Persistence.Data
{
    public class BaseUserDbContext : DbContext
    {
        public BaseUserDbContext(DbContextOptions<BaseUserDbContext> options) : base(options)
        {

        }

        public DbSet<User> User{get;set;}
    }
}
