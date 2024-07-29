using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CMS.Domain.Entities.Identity;

namespace CMS.Infrastructure.Configuration
{
    public class PermissionConfig : LookupConfig<Permission, int>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Id)
                .ValueGeneratedNever();
         
        }
    }
}