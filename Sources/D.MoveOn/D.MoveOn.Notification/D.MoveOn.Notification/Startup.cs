using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using D.MoveOn.Common.Consul;
using D.MoveOn.Common;
using D.MoveOn.Notification.Infra;
using Microsoft.AspNetCore.SignalR.Redis;
using D.MoveOn.Notification.Hubs;
using D.MoveOn.Common.Fabio;
using Autofac;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Common.Dispatchers;
using D.MoveOn.Common.Mvc;
using D.MoveOn.Notification.Messages.Events;
using D.MoveOn.Common.Tracer;

namespace D.MoveOn.Notification
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
            services.AddCustomMvc();
            //services.AddControllers();
            services.AddConsulServices();
            services.AddFabio();
            services.AddTracerService();
            AddSignalR(services);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();

            builder.AddRabbitMQServices();
            builder.AddDispatcherServices();
        }

        private void AddSignalR(IServiceCollection services)
        {
            services.AddSignalR()
        .AddStackExchangeRedis(options => {
            options.Configuration.ChannelPrefix = "ChannelName";
            options.Configuration.EndPoints.Add("localhost", 6379);
            options.Configuration.ClientName = "ClientNameSignalR";
            options.Configuration.AllowAdmin = true;
        });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<LufHub>("/message");
            });

            app.UseConsul(applicationLifetime);
            app.UseRabbitMQ().SubscribeEvent<ResultProcess>(@namespace: "order");
        }
    }
}
