using CMS.Domain.Entities.Audit;
using CMS.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Context
{
    public partial class CMSDbContext
    {

        public virtual DbSet<Audit> AuditTrails { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {

            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entries = ChangeTracker.Entries();
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry in entries)
            {
                if (entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.PropertyEntry property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (propertyName == "CreatedById")
                            {
                                property.CurrentValue = _claimService?.UserId;
                            }
                            else if (propertyName == "CreatedDate")
                            {
                                property.CurrentValue = DateTime.UtcNow;
                            }

                            break;
                        case EntityState.Modified:
                            if (propertyName == "ModifiedById")
                            {
                                property.CurrentValue = _claimService?.UserId;
                            }
                            else if (propertyName == "ModifiedDate")
                            {
                                property.CurrentValue = DateTime.UtcNow;
                            }

                            break;
                    }
                }

            }
            OnBeforeSaveChanges();
            return await base.SaveChangesAsync(true, cancellationToken);
        }


        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            List<AuditEntry> auditEntries = [];
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                string userId = _claimService?.UserId;
                AuditEntry auditEntry = new(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId,
                };


                auditEntries.Add(auditEntry);
                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.PropertyEntry property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (AuditEntry auditEntry in auditEntries)
            {
                _ = AuditTrails.Add(auditEntry.ToAudit());
            }
        }
    }
}
