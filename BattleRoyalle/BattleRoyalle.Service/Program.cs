using Topshelf;

namespace BattleRoyalle.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //var serviceProvider = DependencyInjection.BuildServiceProvider();

            //serviceProvider.GetService<Startup>().StartServices();

            HostFactory.Run(x =>
            {
                x.Service<AppBootstrap>(service => {
                    service.ConstructUsing(s => new AppBootstrap());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });

                x.RunAsLocalSystem();
                x.SetServiceName("RemoteControl");
                x.SetDisplayName("Remote Control");
                x.SetDescription("Remote Control with SignalR");
            });
        }
    }
}
