using Microsoft.EntityFrameworkCore;
using CMS.Domain.Entities.Identity;

namespace CMS.Infrastructure.Context
{
    public partial class CMSDbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

    }
}
