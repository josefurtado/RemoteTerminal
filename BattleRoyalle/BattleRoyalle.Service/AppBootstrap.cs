using Microsoft.Extensions.DependencyInjection;

namespace BattleRoyalle.Service
{
    public class AppBootstrap
    {
        private readonly ServiceProvider _serviceProvider;

        public AppBootstrap()
        {
            _serviceProvider = DependencyInjection.BuildServiceProvider();
        }

        public void Start()
        {
            _serviceProvider.GetService<Startup>().StartServices();
        }

        public void Stop()
        {
            _serviceProvider.GetService<Startup>().StopServices();
        }
    }
}
