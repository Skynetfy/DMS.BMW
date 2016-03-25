using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using DMS.Entities.SQLEntites;

namespace DMS.SQLRepository.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User Add(User entity)
        {
            try
            {
                var sql = @"INSERT INTO DMS_User([UserName],[DisplayName],[PassWord],[Email],[Phone],[Status])
                          VALUES(@UserName,@DisplayName,@PassWord,@Email,@Phone,@Status)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@username", DbType.AnsiString, entity.UserName);
                db.AddInParameter(cmd, "@DisplayName", DbType.String, entity.DisplayName);
                db.AddInParameter(cmd, "@PassWord", DbType.AnsiString, entity.Password);
                db.AddInParameter(cmd, "@Email", DbType.AnsiString, entity.Email);
                db.AddInParameter(cmd, "@Phone", DbType.AnsiString, entity.PhoneNo);
                db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
                db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public bool Existe(string name)
        {
            var ret = 0;
            try
            {
                var sql = @"Select count(id)
                           from DMS_User 
                          where UserName=@username and IsDelete=0";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@username", DbType.AnsiString, name);
                ret = (int)db.ExecuteScalar(cmd);
            }
            catch (Exception)
            {
                throw;
            }
            return ret > 0;
        }

        public User CheckUserLogin(string name, string password)
        {
            User user = null;
            try
            {
                var sql = @"Select *
                           from DMS_User 
                          where UserName=@username and Password=@Password and IsDelete=0";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@username", DbType.AnsiString, name);
                db.AddInParameter(cmd, "@Password", DbType.AnsiString, password);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                        user = new User()
                        {
                            Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                            UserName = dt.Rows[0]["UserName"].ToString(),
                            DisplayName = dt.Rows[0]["DisplayName"].ToString(),
                            Password = dt.Rows[0]["PassWord"].ToString(),
                            Email = dt.Rows[0]["Email"].ToString(),
                            PhoneNo = dt.Rows[0]["Phone"].ToString(),
                            Status = Convert.ToInt32(dt.Rows[0]["Status"]),
                            IsDelete = Convert.ToBoolean(dt.Rows[0]["IsDelete"]),
                            LastModifyDate = Convert.ToDateTime(dt.Rows[0]["ModifyDate"]),
                            CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"])
                        };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return user;
        }

        public IList<User> GetPageList(string search)
        {
            var list = new List<User>();
            try
            {
                var sql = string.Format(@"Select *
                           from DMS_User 
                           where IsDelete=0 {0}", search);
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new User()
                        {
                            Id = x.Field<Int64>("Id"),
                            UserName = x.Field<string>("UserName"),
                            DisplayName = x.Field<string>("DisplayName"),
                            Password = x.Field<string>("PassWord"),
                            Email = x.Field<string>("Email"),
                            PhoneNo = x.Field<string>("Phone"),
                            Status = x.Field<int>("Status"),
                            IsDelete = x.Field<bool>("IsDelete"),
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
                var sql = string.Format(@"Select count(Id)
                           from DMS_User 
                           where IsDelete=0 {0}", search);
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
                var sql = @"UPDATE DMS_User SET IsDelete=1
                           WHERE Id=@Id";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Id", DbType.Int64, id);
                count = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return count;
        }

        public User GetUser(Int64 id)
        {
            var list = new User();
            try
            {
                var sql = @"SELECT * FROM DMS_User 
                           WHERE Id=@Id";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Id", DbType.Int64, id);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    list = new User()
                    {
                        Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                        UserName = dt.Rows[0]["UserName"].ToString(),
                        DisplayName = dt.Rows[0]["DisplayName"].ToString(),
                        Password = dt.Rows[0]["PassWord"].ToString(),
                        Email = dt.Rows[0]["Email"].ToString(),
                        PhoneNo = dt.Rows[0]["Phone"].ToString(),
                        Status = Convert.ToInt32(dt.Rows[0]["Status"]),
                        IsDelete = Convert.ToBoolean(dt.Rows[0]["IsDelete"]),
                        LastModifyDate = Convert.ToDateTime(dt.Rows[0]["ModifyDate"]),
                        CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"])
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return list;
        }
    }
}
