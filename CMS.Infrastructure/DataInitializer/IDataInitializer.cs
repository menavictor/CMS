using CMS.Domain.Entities.Lookup;
using System.Collections.Generic;
using CMS.Domain.Entities.Identity;

namespace CMS.Infrastructure.DataInitializer
{
    public interface IDataInitializer
    {
        IEnumerable<Role> SeedRoles();

        IEnumerable<User> SeedUsers();

        IEnumerable<Permission> SeedPermissionsAsync();

        IEnumerable<Action> SeedActionsAsync();

        IEnumerable<Status> SeedStatusesAsync();

    }
}