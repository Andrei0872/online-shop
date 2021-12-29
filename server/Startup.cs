using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using server.Data;
using server.Repositories.ProductRepository;
using server.Seed;
using server.Models;
using server.Helpers.Constants;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

using server.DAL.UnitOfWork;

namespace server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ServerContext>(
                opts => opts.UseSqlServer("Server=localhost;Database=online_shop;User Id=sa;Password=Foo_bar123")
            );

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ServerContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRoleType.Admin, policy => policy.RequireRole(UserRoleType.Admin));
                options.AddPolicy(UserRoleType.User, policy => policy.RequireRole(UserRoleType.User));
            });

            services.Configure<IdentityOptions>(opts => {
                // Using lenient constraints for demo purposes.
                opts.Password.RequiredLength = 3;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
            });

            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SeedDb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, SeedDb seed, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try {
                seed.SeedRoles().Wait();
                seed.SeedUsers().Wait();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
