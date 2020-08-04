using Authorize.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Authorize.Infrastructure.Persistence.EF.Configurations.Applications
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Applications.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Applications.Application> builder)
        {
            builder.HasKey(a => a.Name);
            builder.Property(a => a.Description)
                .HasMaxLength(200);
            
            var converter = new ValueConverter<Version, string>(
                    v => v.ToString(),
                    v => Version.For(v));

            builder.Property(a => a.Version)
                .HasConversion(converter);

            builder.OwnsMany(a => a.Permissions);
        }
    }
}
