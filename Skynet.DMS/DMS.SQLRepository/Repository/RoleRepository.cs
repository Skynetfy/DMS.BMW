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
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public Role Add(Role entity)
        {
            try
            {
                string sql = @"INSERT INTO DMS_Role(RoleName,MenuIds)
                          VALUES(@roleName,@NenuIds)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@roleName", DbType.AnsiString, entity.RoleName);
                db.AddInParameter(cmd, "@NenuIds", DbType.AnsiString, entity.MenuIds);
                var ds = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public List<Role> GetListByIds(IList<string> ids)
        {
            var list = new List<Role>();
            try
            {
                string sql = string.Format(@"SELECT Id,RoleName,MenuIds
                           FROM DMS_Role WHERE IsDelete=0 AND Id IN({0})", string.Join(",", ids));
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Role()
                    {
                        Id = x.Field<Int64>("Id"),
                        RoleName = x.Field<string>("RoleName"),
                        MenuIds = x.Field<string>("MenuIds")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return list;
        }

        public List<Role> GetPageList(string search)
        {
            var list = new List<Role>();
            try
            {
                string sql = string.Format(@"SELECT Id,RoleName,MenuIds,ModifyDate,CreateDate
                           FROM DMS_Role WHERE IsDelete=0 {0}", search);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Role()
                    {
                        Id = x.Field<Int64>("Id"),
                        RoleName = x.Field<string>("RoleName"),
                        MenuIds = x.Field<string>("MenuIds"),
                        LastModifyDate = x.Field<DateTime>("ModifyDate"),
                        CreateDate = x.Field<DateTime>("CreateDate")
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return list;
        }

        public int GetPageCount(string search)
        {
            var count = 0;
            try
            {
                string sql = string.Format(@"SELECT count(Id)
                           FROM DMS_Role WHERE IsDelete=0 {0}", search);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                count = (int)db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return count;
        }

        public int Delete(Int64 id)
        {
            var count = 0;
            try
            {
                string sql = string.Format(@"UPDATE DMS_Role
                           SET IsDelete=1 WHERE Id=@id");
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@id", DbType.Int64, id);
                count = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return count;
        }

        public Role GetRole(Int64 id)
        {
            var role = new Role();
            try
            {
                string sql = string.Format(@"SELECT Id,[RoleName],[MenuIds]
                           FROM DMS_Role WHERE IsDelete=0 AND Id=@Id");
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@id", DbType.Int64, id);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    role = new Role()
                    {
                        Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                        RoleName = dt.Rows[0]["RoleName"].ToString(),
                        MenuIds = dt.Rows[0]["MenuIds"].ToString()
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

        public Role Update(Role entity)
        {
            try
            {
                string sql = string.Format(@"UPDATE DMS_Role SET [RoleName]=@RoleName,[MenuIds]=@MenuIds
                            WHERE Id=@Id");
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@id", DbType.Int64, entity.Id);
                db.AddInParameter(cmd, "@RoleName", DbType.String, entity.RoleName);
                db.AddInParameter(cmd, "@MenuIds", DbType.AnsiString, entity.MenuIds);
                var ds = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }
    }
}
