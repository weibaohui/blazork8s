using System;
using System.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using server.Service.K8s;

namespace server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory Factory       { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region db

            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.PostgreSQL, @"Host=127.0.0.1;Port=5432;Username=postgres;Password=postgres; Database=blazork8s;Pooling=true;Minimum Pool Size=1")
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                .UseMonitorCommand(cmd =>
                {
                    Console.WriteLine(cmd.CommandText);
                    foreach (DbParameter p in cmd.Parameters)
                    {
                        Console.WriteLine($"{p.DbType},{p.ParameterName}={p.Value}");
                    }
                })
                .Build(); //请务必定义成 Singleton 单例模式
            services.AddSingleton(fsql);

            #endregion

            #region CORS

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => { builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader(); });
            });

            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "server", Version = "v1"}); });
            services.AddSingleton<Watcher>();
            services.AddSingleton<PodWatcher>();
            services.AddSingleton<NodeWatcher>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}
