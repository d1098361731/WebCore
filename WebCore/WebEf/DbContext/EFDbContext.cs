using Common;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebEf
{
    public class EFDbContext: DbContext
    {
        public EFDbContext():this(new DbContextOptions<EFDbContext> ()) {  }

        public EFDbContext(DbContextOptions<EFDbContext> optons)
        {

        }

        public virtual DbSet<Love> Loves { get; set; }

        public  virtual  DbSet<Advertisement> Advertisements { get; set; }

        public virtual DbSet<BlogArticle> BlogArticles { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        public  virtual  DbSet<User> Users { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public virtual DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 角色菜单
        /// </summary>
        public virtual DbSet<RoleMenu> RoleMenus { get; set; }


        /// <summary>
        /// 角色
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 用户绑定角色
        /// </summary>
        public virtual DbSet<UserRole> UserRoles { get; set; }



        /// <summary>
        /// 重写此方法以配置要使用的数据库（和其他选项）用于此上下文。对于已创建。基本实现什么也不做。
        /// </summary>
        /// <param name="optionsBuilder">数据库连接配置用于上下文</param>
        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //使用sql server数据库
            optionsBuilder.UseSqlServer(AppSettingsHelpper.ConnectionString);
        }

        /// <summary>
        /// 创建派生上下文的第一个实例时仅调用此方法一次。 然后将缓存该上下文的模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Seed(modelBuilder);
        }

        /// <summary>
        /// 迁移种子数据
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                Password = "admin",
                State = 1,
                Address = "管理员测试地址",
                Company = "西安新时间",
                Remarks = ""
            });
            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Main",
                Label = "主界面",
                Path = "/",
                Icon = "el-icon-s-custom",
                Index = 0,
                ParentMentId = null,
                Tooptip = "主界面提示",
                Remarks = ""
            });

            modelBuilder.Entity<Menu>().HasData(new Menu
            {
                Id = Guid.NewGuid(),
                Name = "userManager",
                Label = "成员管理",
                Path = "/userManager",
                Icon = "el-icon-s-custom",
                Index = 1,
                ParentMentId = null,
                Tooptip = "成员管理",
                Remarks = ""
            });

        }
    }
}
