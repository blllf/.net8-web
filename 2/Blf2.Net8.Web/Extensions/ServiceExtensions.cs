using Blf.Net8.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Blf2.Net8.Web.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureCors(this IServiceCollection services) {
            services.AddCors(options =>
            {
                options.AddPolicy("AnyPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        //向 DI 容器注册了 GameManagermentDbContext 类型，并指定了如何配置其 DbContextOptions。
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration) {
            var connectiongString = configuration.GetConnectionString("WebApiDatabase");
            //注册 DbContext 并配置数据库提供程序
            services.AddDbContext<GameManagermentDbContext>(options => options.UseNpgsql(connectiongString));
        }
    }

    
}
