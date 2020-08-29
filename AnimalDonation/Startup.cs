using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnimalDonation.DataAccessLayer.Interfaces;
using AnimalDonation.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using AnimalDonation.Core.Services;
using AnimalDonation.Core.Interfaces;
using AnimalDonation.DataAccessLayer.Repository;
using FluentValidation.AspNetCore;

namespace AnimalDonation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                 .AddFluentValidation(s =>
                 {
                     s.RegisterValidatorsFromAssemblyContaining<Startup>();
                     s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                 });



            services.AddTransient<IOrderService ,OrderService>();
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            services.AddHttpContextAccessor();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Animal"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=CreateDonation}/{id?}");
            });
        }
    }
}
