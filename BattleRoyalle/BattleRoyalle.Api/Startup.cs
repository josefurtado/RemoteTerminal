using BattleRoyalle.Api.SignalR;
using BattleRoyalle.Application.Services;
using BattleRoyalle.Domain.Handlers;
using BattleRoyalle.Domain.Interfaces.Services;
using BattleRoyalle.Domain.Interfaces.UnitOfWork;
using BattleRoyalle.Infrastructure.Data.Contexts;
using BattleRoyalle.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BattleRoyalle.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BattleRoyalle API", Version = "v1" });
            });

            services.AddDbContext<BattleRoyalleContext>( options => options.UseInMemoryDatabase("BattleRoyalle"));

            services.AddScoped<IUnitOfWork, UnityOfWork<BattleRoyalleContext>>();
            services.AddScoped<IMachineService, MachineService>();
            services.AddTransient<MachineHandler, MachineHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BattleRoyalleHub>("/battleRoyalleHub");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BattleRoyalle API V1");
            });
        }
    }
}
