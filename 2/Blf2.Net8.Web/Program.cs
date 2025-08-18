
using Blf.Net8.Contracts;
using Blf.Net8.EntityFramework.Repositories;
using Blf2.Net8.Entitry;
using Blf2.Net8.EntityFramework.Repositories;
using Blf2.Net8.Service;
using Blf2.Net8.Service.Impl;
using Blf2.Net8.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Blf2.Net8.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // ��IPlayerRepository�ӿ���PlayerRepositoryʵ�������ע��󶨡�AddScoped��ʾע��Ϊ�������������ڵķ���
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
       
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option => {
                // ע��չʾ
                string basePath = AppContext.BaseDirectory;
                string xmlPath = Path.Combine(basePath, "Blf2.Net8.Web.xml");
                option.IncludeXmlComments(xmlPath);

                #region ֧��token��ֵ
                {
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                        Description = "JWT��Ȩ(����������ͷ��)",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"

                    });

                    //��Ӱ�ȫҪ��
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                }
                #endregion
            });
            builder.Services.ConfigureCors();
            builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());    //ָ������

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AnyPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
