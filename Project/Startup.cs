﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Data;
using Project.Repositories;
using Project.Repositories.Interface;

namespace Project
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
            services.AddAutoMapper();

            services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<DataContext>();
            /*   services
               .AddAuthentication()
               .AddCookie("Public_Scheme", options =>
               {    
                   options.LoginPath = "/Values/login";
                   options.LogoutPath = "/Values/logout";
               }).AddCookie("Management_Scheme", options =>
               {
                   options.LoginPath = "/Values/login";
                   options.Cookie.Name = "MarahShabib";
                  // options.LogoutPath = "/management/logout";
               });*/


            services
.AddAuthentication(op =>
{
    op.DefaultScheme = "Management_Scheme";
})
.AddCookie("Management_Scheme", options =>
{
    options.LoginPath = "/Values/login1";
    options.Cookie.Name = "Maraaaah";
    options.LogoutPath = "/Values/logout";
});
            services
.AddAuthentication(op =>
{
    op.DefaultScheme = "Admin";
})
.AddCookie("Admin", options =>
{
    options.LoginPath = "/admin/Messagelogin";
    options.Cookie.Name = "Admin";
    options.LogoutPath = "/Values/logout";
});

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISecurityManager, SecurityManager>();
            services.AddTransient<IFileRepository, FileRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCookiePolicy();

         

            app.UseMvc();

          
        }
    }
}