using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Infrastructure.Data.EntityConfigurations.Generic;
using BattleRoyalle.Infrastructure.Data.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoyalle.Infrastructure.Data.EntityConfigurations.Entities
{
    public class DiskConfiguration : GenericConfiguration<Disk>, IEntityTypeConfiguration<Disk>, IEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<Disk> builder)
        {
            DefaultConfiguration(builder: builder, tableName: "Disks");

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.FreeDiskSpace)
                .IsRequired();

            builder.Property(x => x.TotalDiskSize)
                .IsRequired();
        }
    }
}
