using BattleRoyalle.Service.Core;
using BattleRoyalle.Service.Core.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BattleRoyalle.Service
{
    public static class DependencyInjection
    {
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<Startup>();

            services.AddSingleton<ISignalRService, SignalRService>();

            services.AddSingleton<IPowerShellProcess, PowerShellProcess>();

            services.AddSingleton<HubConnection>(
                new HubConnectionBuilder()
                    .WithUrl("http://localhost:50198/battleRoyalleHub")
                    .WithAutomaticReconnect(
                            new[] {
                                TimeSpan.Zero,
                                TimeSpan.FromSeconds(2),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(10),
                                TimeSpan.FromSeconds(30),
                                TimeSpan.FromSeconds(60)
                            })
                    .Build());

            return services;
        }

        public static ServiceProvider BuildServiceProvider()
        {
            return ConfigureServices().BuildServiceProvider();
        }
    }
}
