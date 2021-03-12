using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PontualAPI.Data;

namespace PontualAPI
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PontualDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServer")));
            services.AddScoped<PontualDataContext, PontualDataContext>();
            services.AddControllers();
            services.AddSwaggerGen(s => 
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Pontual API", Version = "v1" });
            });
            services.AddCors(c => c.AddPolicy("EnableAllCrossOriginRequests", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors("EnableAllCrossOriginRequests");
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(s => {
                s.RoutePrefix = string.Empty;
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
