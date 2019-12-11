using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D.MoveOn.Common.Consul;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Common.Fabio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using D.MoveOn.Common.Dispatchers;
using D.MoveOn.Common.Mvc;
using D.MoveOn.Common.Mongo;
using D.MoveOn.Order.Messages.Commands;
using D.MoveOn.Common;
using D.MoveOn.Common.Tracer;

namespace D.MoveOn.Order
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddInitializers(typeof(IMongoDbInitializer));
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
            builder.AddMongoServices();
            builder.AddMongoRepository<D.MoveOn.Order.Domain.Order>("Orders");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostApplicationLifetime applicationLifetime,
            IHostEnvironment env, IStartupInitializer startupInitializer)
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
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // app.UseHttpsRedirection();
            app.UseMvc();

            app.UseConsul(applicationLifetime);
            app.UseRabbitMQ().SubscribeCommand<CreateOrderCommand>();
            startupInitializer.InitializeAsync();
        }
    }
}