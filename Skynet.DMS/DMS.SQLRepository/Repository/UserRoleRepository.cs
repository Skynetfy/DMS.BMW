using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository.Repository
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        public UserRole Add(UserRole entity)
        {
            try
            {
                string sql = @"Insert INTO DMS_UserRole
                          (UserId,RoleIds)
                         VALUES(@userid,@RoleIds)
                         ";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@userid", DbType.Int64, entity.UserId);
                db.AddInParameter(cmd, "@RoleIds", DbType.AnsiString, entity.RoleIds);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public UserRole GetByUid(long uid)
        {
            UserRole role = null;
            try
            {
                string sql = @"SELECT TOP 1 Id,UserId,RoleIds FROM DMS_UserRole WHERE IsDelete=0 AND UserId=@uid";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@uid", DbType.Int64, uid);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                        role = new UserRole()
                        {
                            Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                            UserId = Convert.ToInt32(dt.Rows[0]["UserId"]),
                            RoleIds = dt.Rows[0]["RoleIds"].ToString()
                        };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return role;
        }

        public int Update(UserRole entity)
        {
            var i = 0;
            try
            {
                string sql = @"UPDATE DMS_UserRole
                          SET RoleIds=@RoleIds WHERE Id=@Id
                         ";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Id", DbType.Int64, entity.Id);
                db.AddInParameter(cmd, "@RoleIds", DbType.AnsiString, entity.RoleIds);
                i = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            return i;
        }

        public long Exsite(long uid)
        {
            long i = 0;
            try
            {
                string sql = @"SELECT TOP 1 Id FROM DMS_UserRole WHERE IsDelete=0 AND UserId=@uid";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@uid", DbType.Int64, uid);
                var ds = db.ExecuteScalar(cmd);
                i = ds != null ? Convert.ToInt64(ds) : 0;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            return i;
        }
    }
}
