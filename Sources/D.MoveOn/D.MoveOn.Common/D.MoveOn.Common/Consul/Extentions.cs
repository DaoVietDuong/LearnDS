using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using D.MoveOn.Common;
using D.MoveOn.Common.Mvc;
using D.MoveOn.Common.Fabio;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace D.MoveOn.Common.Consul
{
    public static class Extentions
    {
        private const string ConsultSection = "consul";
        private const string FabioSection = "fabio";

        public static void AddConsulServices(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var consulOption = configuration.GetOptions<ConsulOption>(ConsultSection);
            services.Configure<FabioOptions>(configuration.GetSection(FabioSection));
            services.Configure<ConsulOption>(configuration.GetSection(ConsultSection));
            services.AddTransient<IConsulServicesRegistry, ConsulServicesRegistry>();
            services.AddTransient<ConsulServiceDiscoveryMessageHandler>();
            services.AddHttpClient<IConsulHttpClient, ConsulHttpClient>()
                .AddHttpMessageHandler<ConsulServiceDiscoveryMessageHandler>();
            services.AddSingleton<IConsulClient, ConsulClient>(c => new ConsulClient(conf =>
            {
                if (!string.IsNullOrEmpty(consulOption.Url))
                {
                    conf.Address = new Uri(consulOption.Url);
                }
            }));
        }

        public static void UseConsul(this IApplicationBuilder app,
        IHostApplicationLifetime applicationLifetime)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetService<IConfiguration>();
                var consulOption = configuration.GetOptions<ConsulOption>(ConsultSection);
                var fabioOptions = configuration.GetOptions<FabioOptions>(FabioSection);
                var serivceInfor = scope.ServiceProvider.GetService<IServiceInfor>();

                if (!consulOption.Enable)
                {
                    return;
                }

                var client = scope.ServiceProvider.GetService<IConsulClient>();
                var id = serivceInfor.Id;
                var name = consulOption.Name;
                var uri = new Uri($"{consulOption.Address}{ (consulOption.Port == 0 ? string.Empty : $":{consulOption.Port}") }");
                var address = consulOption.Address;
                //var uri = new Uri(address);

                var pingEndpoint = consulOption.PingEndPoint;
                var pingInterval = consulOption.PingInterval <= 0 ? 5 : consulOption.PingInterval;
                var removeAfterInterval =
                    consulOption.RemoveAfterInterval <= 0 ? 10 : consulOption.RemoveAfterInterval;
                var registerAgent = new AgentServiceRegistration()
                {
                    ID = id,
                    Name = name,
                    Address = address,
                    Port = consulOption.Port,
                    Tags = fabioOptions.Enabled ? GetFabioTags(name, fabioOptions.Service) : null,
                };

                if (consulOption.PingEnabled || fabioOptions.Enabled)
                {
                    var scheme = address.StartsWith("http",
                                                            StringComparison.InvariantCultureIgnoreCase)
                        ? string.Empty
                        : "http://";
                    var check = new AgentServiceCheck(){
                            Interval = TimeSpan.FromSeconds(consulOption.Interval),
                            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulOption.RemoveAfterInterval),
                            HTTP = $"{scheme}{consulOption.Address}{(consulOption.Port == 0 ? string.Empty : $":{consulOption.Port}") }/{consulOption.PingEndPoint}"
                        };
                    registerAgent.Checks = new[] { check };
                }

                client.Agent.ServiceRegister(registerAgent).Wait();

                applicationLifetime.ApplicationStopped.Register(() =>
                {
                    client.Agent.ServiceDeregister(id);
                });
            }
        }

        private static string[] GetFabioTags(string consulService, string fabioService)
        {
            var service = (string.IsNullOrWhiteSpace(fabioService) ? consulService : fabioService)
                .ToLowerInvariant();

            return new[] { $"urlprefix-/{service} strip=/{service}" };
        }
    }
}