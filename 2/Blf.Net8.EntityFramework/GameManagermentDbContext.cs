using Blf.Net8.EntityFramework.Mappings;
using Blf2.Net8.Entitry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace Blf.Net8.EntityFramework {
    public class GameManagermentDbContext : DbContext {
        //连接数据库的“钥匙”
        //构造函数负责接收并使用这把“钥匙”来建立数据库连接
        //它包含了连接数据库所需的信息（比如连接字符串、使用哪种数据库如 SQL Server、SQLite 等）。
        public GameManagermentDbContext(DbContextOptions<GameManagermentDbContext> options) : base(options)
        {
        }


        //DbSet<T> 是一个实体集合的代理，代表数据库中的一张表。
        //每一个 DbSet<T> 对应一张数据库表。
        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //自动扫描程序集中的所有 IEntityTypeConfiguration 配置类（推荐)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //显式添加
            //modelBuilder.ApplyConfiguration(new PlayerMap());

            //在DbContext中配置(简单直接) 适用于少量的数据种子
            // 添加 Player 种子数据（使用固定 Guid）
            var adminPlayerId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userPlayerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var seedDataCreationTime = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc); // 固定时间

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = adminPlayerId, Account = "admin" , AccountType = "Admin" , DateCreate = seedDataCreationTime },
                new Player { Id = userPlayerId, Account = "user" , AccountType = "User" , DateCreate = seedDataCreationTime }
                );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), NickName = "Warrior", Classes = "Warrior" , Level = 50, DateCreated = seedDataCreationTime, PlayerId = adminPlayerId },
                new Character { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), NickName = "Mage", Classes = "Mage", Level = 45, DateCreated = seedDataCreationTime, PlayerId = adminPlayerId },
                new Character { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), NickName = "Rogue", Classes = "Rogue", Level = 30, DateCreated = seedDataCreationTime, PlayerId = userPlayerId }
                );   

        }

    }
}
