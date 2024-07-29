using CMS.Domain.Entities.Lookup;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Context
{
    public partial class CMSDbContext
    {

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

    }
}
