using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Infrastructure.Data.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BattleRoyalle.Infrastructure.Data.Contexts
{
    public class BattleRoyalleContext : DbContext
    {
        public BattleRoyalleContext(DbContextOptions<BattleRoyalleContext> options)
            : base(options) { }

        public DbSet<Disk> Disks { get; set; }
        public DbSet<Machine> Machines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityConfigurations = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityConfiguration).IsAssignableFrom(x) && !x.IsAbstract);

            foreach (var entity in entityConfigurations)
            {
                dynamic configurationInstance = Activator.CreateInstance(entity);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
