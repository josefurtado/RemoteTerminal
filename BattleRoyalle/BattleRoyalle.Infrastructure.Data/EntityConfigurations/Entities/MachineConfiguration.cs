using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Infrastructure.Data.EntityConfigurations.Generic;
using BattleRoyalle.Infrastructure.Data.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoyalle.Infrastructure.Data.EntityConfigurations.Entities
{
    public class MachineConfiguration : GenericConfiguration<Machine>, IEntityTypeConfiguration<Machine>, IEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            DefaultConfiguration(builder: builder, tableName: "Machines");

            builder.Property(x => x.Name);

            builder.Property(x => x.OSVersion);

            builder.Property(x => x.IpAddress);

            builder.Property(x => x.LastConnectionId);

            builder
                .HasMany(x => x.Disks)
                .WithOne(x => x.Machine)
                .IsRequired();
        }
    }
}
