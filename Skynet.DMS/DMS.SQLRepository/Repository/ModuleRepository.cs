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
    public class ModuleRepository : BaseRepository, IModuleRepository
    {
        public Module Add(Module entity)
        {
            try
            {
                string sql = @"INSERT INTO DMS_Module(
                               ModuleName,Url,IsUse,
                               Icon,ParentId,Level,OrderBy)
                               VALUES(@ModuleName,@Url,@IsUse,@Icon,@ParentId,
                               @Level,@OrderBy)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@ModuleName", DbType.AnsiString, entity.ModuleName);
                db.AddInParameter(cmd, "@Url", DbType.AnsiString, entity.Url);
                db.AddInParameter(cmd, "@Icon", DbType.AnsiString, entity.Icon);
                db.AddInParameter(cmd, "@ParentId", DbType.Int64, entity.ParentId);
                db.AddInParameter(cmd, "@Level", DbType.Int32, entity.Level);
                db.AddInParameter(cmd, "@OrderBy", DbType.Int32, entity.OrderBy);
                db.AddInParameter(cmd, "@IsUse", DbType.Boolean, entity.IsUse);
                var ds = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public List<Module> GetPageList()
        {
            var list = new List<Module>();
            try
            {
                var sql = @"SELECT Id,ModuleName,Url
                           ,Icon,ParentId,IsUse,Level,OrderBy
                           ,ModifyDate,CreateDate
                           FROM DMS_Module WHERE IsDelete=0 AND IsUse=1";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Module()
                    {
                        Id = x.Field<Int64>("Id"),
                        ModuleName = x.Field<string>("ModuleName"),
                        Url = x.Field<string>("Url"),
                        Icon = x.Field<string>("Icon"),
                        ParentId = x.Field<int>("ParentId"),
                        IsUse = x.Field<bool>("IsUse"),
                        Level = x.Field<int>("Level"),
                        OrderBy = x.Field<int>("OrderBy"),
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

        public List<Module> GetListByIds(IList<string> ids)
        {
            var list = new List<Module>();
            try
            {
                var sql = string.Format(@"SELECT Id,ModuleName,Url
                           ,Icon,ParentId,IsUse,Level,OrderBy
                           ,ModifyDate,CreateDate
                           FROM DMS_Module WHERE IsDelete=0 AND Id IN({0}) AND IsUse=1", string.Join(",", ids));
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Module()
                    {
                        Id = x.Field<Int64>("Id"),
                        ModuleName = x.Field<string>("ModuleName"),
                        Url = x.Field<string>("Url"),
                        Icon = x.Field<string>("Icon"),
                        ParentId = x.Field<int>("ParentId"),
                        IsUse = x.Field<bool>("IsUse"),
                        Level = x.Field<int>("Level"),
                        OrderBy = x.Field<int>("OrderBy"),
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

        public List<Module> GetParentList()
        {
            var list = new List<Module>();
            try
            {
                var sql = @"SELECT Id,ModuleName,Url
                           ,Icon,ParentId,IsUse,Level,OrderBy
                           ,ModifyDate,CreateDate
                           FROM DMS_Module WHERE IsDelete=0 AND Level=1 AND IsUse=1";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Module()
                    {
                        Id = x.Field<Int64>("Id"),
                        ModuleName = x.Field<string>("ModuleName"),
                        Url = x.Field<string>("Url"),
                        Icon = x.Field<string>("Icon"),
                        ParentId = x.Field<int>("ParentId"),
                        IsUse = x.Field<bool>("IsUse"),
                        Level = x.Field<int>("Level"),
                        OrderBy = x.Field<int>("OrderBy"),
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

        public int Delete(Int64 id)
        {
            var count = 0;
            try
            {
                var sql = @"UPDATE DMS_Module SET IsDelete=1,ModifyDate=getdate()
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

        public Module GetModule(Int64 id)
        {
            Module module = null;
            try
            {
                var sql = @"SELECT Id,ModuleName,Url
                           ,Icon,ParentId,IsUse,Level,OrderBy
                           ,ModifyDate,CreateDate
                           FROM DMS_Module WHERE IsDelete=0 AND Id=@Id";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Id", DbType.Int64, id);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    module = new Module()
                    {
                        Id = Convert.ToInt64(dt.Rows[0]["Id"]),
                        ModuleName = dt.Rows[0]["ModuleName"].ToString(),
                        Url = dt.Rows[0]["Url"].ToString(),
                        Icon = dt.Rows[0]["Icon"].ToString(),
                        ParentId = Convert.ToInt64(dt.Rows[0]["ParentId"]),
                        IsUse = Convert.ToBoolean(dt.Rows[0]["IsUse"]),
                        Level = Convert.ToInt32(dt.Rows[0]["Level"]),
                        OrderBy = Convert.ToInt32(dt.Rows[0]["OrderBy"])
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return module;
        }

        public Module Update(Module entity)
        {
            try
            {
                var sql = @"UPDATE DMS_Module SET ModuleName=@ModuleName,Url=@Url
                           ,Icon=@Icon,ParentId=@ParentId,IsUse=@IsUse,Level=@Level
                           ,ModifyDate=getdate()
                           WHERE Id=@Id";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@ModuleName", DbType.String, entity.ModuleName);
                db.AddInParameter(cmd, "@Url", DbType.AnsiString, entity.Url);
                db.AddInParameter(cmd, "@Icon", DbType.AnsiString, entity.Icon);
                db.AddInParameter(cmd, "@ParentId", DbType.Int64, entity.ParentId);
                db.AddInParameter(cmd, "@IsUse", DbType.Boolean, entity.IsUse);
                db.AddInParameter(cmd, "@Level", DbType.Int32, entity.Level);
                db.AddInParameter(cmd, "@Id", DbType.Int64, entity.Id);
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
