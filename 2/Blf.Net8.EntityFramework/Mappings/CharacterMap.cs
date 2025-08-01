using Blf2.Net8.Entitry;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blf.Net8.EntityFramework.Mappings {
    public class CharacterMap : IEntityTypeConfiguration<Character> {
        public void Configure(EntityTypeBuilder<Character> builder) {
            builder.Property(cha => cha.NickName).HasMaxLength(20);
            builder.Property(cha => cha.Classes).HasMaxLength(20);

            builder.HasIndex(cha => cha.NickName).IsUnique();
            builder.HasOne(cha => cha.Player).WithMany(p => p.Characters).HasForeignKey(cha => cha.PlayerId);
        }
    }
}
