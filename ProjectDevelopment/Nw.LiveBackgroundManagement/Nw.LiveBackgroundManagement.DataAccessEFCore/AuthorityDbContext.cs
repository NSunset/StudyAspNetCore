using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models;
using Nw.LiveBackgroundManagement.Common;
using Nw.LiveBackgroundManagement.DataAccessEFCore.Models.Hangfire;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore
{
    public class AuthorityDbContext : DbContext
    {
        //private readonly ConnectionStrings _config;
        //public AuthorityDbContext(IOptions<ConnectionStrings> options)
        //{
        //    _config = options.Value;
        //}

        /// <summary>
        /// DesignTimeDbContextFactory使用：用于CodeFist迁移
        /// 使用迁移时注释OnConfiguring方法
        /// </summary>
        /// <param name="options"></param>
        public AuthorityDbContext(DbContextOptions<AuthorityDbContext> options):base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql(
        //        _config.AuthorityDbContext,
        //        new MySqlServerVersion(new Version(5, 7, 37))
        //        );
        //    //使用动态代理进行延迟加载
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        #region 后台 
        public DbSet<SysLog> SysLog { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysRoleMenuMapping> SysRoleMenuMapping { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysUserMenuMapping> SysUserMenuMapping { get; set; }
        public DbSet<SysUserRoleMapping> SysUserRoleMapping { get; set; }
        #endregion


        #region 前台 
        /// <summary>
        /// 评论回复表
        /// </summary>
        public DbSet<CSCommentReply> CSCommentReply { get; set; }

        public DbSet<CSComment> CSComment { get; set; }

        public DbSet<CSWorks> CSWorks { get; set; }

        public DbSet<CSRoom> CSRoom { get; set; }

        public DbSet<CSRoomType> CSRoomType { get; set; }

        public DbSet<CSScoreList> CSScoreList { get; set; }

        public DbSet<CSUser> CSUser { get; set; }

        public DbSet<CSUserApply> CSUserApply { get; set; }

        public DbSet<CSFollow> CSFollow { get; set; }

        public DbSet<CSIntegralRecharge> CSIntegralRecharge { get; set; }

        public DbSet<CSIntegralRechargeDetail> CSIntegralRechargeDetail { get; set; }



        #endregion

        #region Hangfire

        public DbSet<HangfireUser> HangfireUser { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
