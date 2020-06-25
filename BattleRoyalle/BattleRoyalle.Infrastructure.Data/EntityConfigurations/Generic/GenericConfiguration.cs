using BattleRoyalle.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BattleRoyalle.Infrastructure.Data.EntityConfigurations.Generic
{
    public class GenericConfiguration<T> where T : Entity
    {
        public void DefaultConfiguration(EntityTypeBuilder<T> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreationDate)
                .IsRequired();
        }
    }
}
