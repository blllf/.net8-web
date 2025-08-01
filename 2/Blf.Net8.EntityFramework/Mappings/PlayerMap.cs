using Blf2.Net8.Entitry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf.Net8.EntityFramework.Mappings {
    public class PlayerMap : IEntityTypeConfiguration<Player> {
        public void Configure(EntityTypeBuilder<Player> builder) {
            builder.Property(player => player.Account).HasMaxLength(50);
            builder.Property(player => player.AccountType).HasMaxLength(10);
            builder.HasIndex(player => player.Account).IsUnique();
        }
    }
}
