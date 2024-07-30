using CMS.Common.Services;
using CMS.Domain.Entities.Business;
using CMS.Domain.Entities.Identity;
using CMS.Infrastructure.Configuration;
using CMS.Infrastructure.DataInitializer;
using Microsoft.EntityFrameworkCore;
using Action = CMS.Domain.Entities.Lookup.Action;
using Status = CMS.Domain.Entities.Lookup.Status;

namespace CMS.Infrastructure.Context
{
    public partial class CMSDbContext : DbContext
    {
        private readonly IDataInitializer _dataInitializer;
        private readonly IClaimService _claimService;
        public CMSDbContext(DbContextOptions<CMSDbContext> options, IDataInitializer dataInitializer, IClaimService claimService) : base(options)
        {
            _dataInitializer = dataInitializer;
            _claimService = claimService;
        }


        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<GroupLevel> GroupLevels { get; set; }
        public virtual DbSet<EmployeeGroupLevel> EmployeeGroupLevels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            _ = modelBuilder.ApplyConfiguration(new PermissionConfig());
            _ = modelBuilder.ApplyConfiguration(new ActionConfig());
            _ = modelBuilder.ApplyConfiguration(new StatusConfig());

            _ = modelBuilder.Entity<Role>().HasData(_dataInitializer.SeedRoles());
            _ = modelBuilder.Entity<User>().HasData(_dataInitializer.SeedUsers());
            _ = modelBuilder.Entity<Permission>().HasData(_dataInitializer.SeedPermissionsAsync());
            _ = modelBuilder.Entity<Status>().HasData(_dataInitializer.SeedStatusesAsync());
            _ = modelBuilder.Entity<Action>().HasData(_dataInitializer.SeedActionsAsync());

            base.OnModelCreating(modelBuilder);
        }




    }
}
