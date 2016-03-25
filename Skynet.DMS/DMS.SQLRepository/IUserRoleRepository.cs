
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        UserRole GetByUid(long uid);

        int Update(UserRole entity);

        long Exsite(long uid);
    }
}
