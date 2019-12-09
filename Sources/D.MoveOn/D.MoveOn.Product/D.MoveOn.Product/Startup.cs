using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D.MoveOn.Common.Consul;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Common.Tracer;
using D.MoveOn.Common.Fabio;
using D.MoveOn.Common.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using JD.MoveOn.Product.Messages.Commands;
using D.MoveOn.Common.Dispatchers;

namespace JD.MoveOn.Product
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddConsulServices();
            services.AddTracerService();
            services.AddFabio();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.AddRabbitMQServices();
            builder.AddDispatcherServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostApplicationLifetime applicationLifetime,
            IHostEnvironment env)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (applicationLifetime is null)
            {
                throw new ArgumentNullException(nameof(applicationLifetime));
            }

            if (env is null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseConsul(applicationLifetime);
            app.UseRabbitMQ().SubscribeCommand<CreateItemCommand>();
        }
    }
}