using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QSS.TCCS.Application.Interfaces;
using QSS.TCCS.Application.Services;
using QSS.TCCS.DataAccess.DBContext;
using QSS.TCCS.DataAccess.Interfaces;
using QSS.TCCS.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QSS.TCCS.Api
{
    public class Startup
    {
        private const string Database = "TCCSInMemoryDatabase";
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _currentEnvironment;
        private const string Test = "Test";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QSS.TCCS.Api", Version = "v1" });
            });

            services.AddDbContext<TCCSContext>(item => item.UseSqlServer
              (Configuration.GetConnectionString("TCCSConnection")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QSS.TCCS.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class HostingEnvironmentExtension
    {
        private const string Test = "Test";
        public static bool IsTest(this IWebHostEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(Test);
        }
    }
}
