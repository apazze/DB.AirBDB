using DB.AirBDB.Common.Utils;
using DB.AirBDB.DAL.Repository;
using DB.AirBDB.DAL.Repository.DAO;
using DB.AirBDB.Services.API.Configuration;
using DB.AirBDB.Services.API.Filtros;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DB.AirBDB.Services.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var section = Configuration.GetSection(nameof(ReservasConfiguration));
            var reservasConfiguration = section.Get<ReservasConfiguration>();

            services.AddSingleton<AppDBContext>();
            services.AddSingleton(reservasConfiguration);
            services.AddSingleton<ValidadorDeDatas>();

            services.AddTransient<IUsuarioDAO, UsuarioDAO>();
            services.AddTransient<ILugarDAO, LugarDAO>();
            services.AddTransient<IReservaDAO, ReservaDAO>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI AirBDB", Version = "v1" });
                c.EnableAnnotations();
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ErrorResponseFilter));
            });
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI AirBDB V1");
                opt.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
