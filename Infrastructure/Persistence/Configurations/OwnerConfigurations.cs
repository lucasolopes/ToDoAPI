using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnKanBan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal sealed class OwnerConfigurations : IEntityTypeConfiguration<WhiteBoard>
    {
        public void Configure(EntityTypeBuilder<WhiteBoard> builder)
        {
            builder.ToTable(nameof(WhiteBoard));
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();
            builder.Property(w => w.Name).HasMaxLength(60).IsRequired();
        }
    }
}
