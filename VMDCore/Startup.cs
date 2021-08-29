using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Bussiness.Managers;
using VMDCore.Data;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Repositories;
using VMDCore.Extensions;

namespace VMDCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private readonly string _contentRootPath;

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = DataUtils.ResolveDbConnectionString(Configuration, _contentRootPath);
            /*  services.AddDbContext<VmdDbContext>(options =>
                  options.UseLazyLoadingProxies().UseSqlServer(
                      Configuration.GetConnectionString("DefaultConnection"))); */
            services.AddDbContext<VmdDbContext>(options =>
                  options.UseLazyLoadingProxies().UseSqlServer(dbConnectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IDrinkRepository, DrinkRepository>();
            services.AddScoped<ICoinRepository, CoinRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();

            services.AddScoped<IDrinkManager, DrinkManager>();
            services.AddScoped<ICoinManager, CoinManager>();
            services.AddScoped<IOperationManager, OperationManager>();


            services.AddMvc();
            services.AddRazorPages();          

            services.AddControllersWithViews();

            services.AddImageProcessing();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOperationManager operationManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var operation = operationManager.FindOperationById(1);
            operation.Balance = 0;
            operation.Message = "";
            operationManager.SaveOperation(operation);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
