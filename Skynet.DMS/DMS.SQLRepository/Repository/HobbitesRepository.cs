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
    public class HobbitesRepository:BaseRepository,IHobbitesRepository
    {
        public Hobbites Add(Hobbites entity)
        {
            try
            {
                string sql = @"INSERT INTO DMS_Hobbites(
                               TypeId,HbName,HbDesc,ImageUrl)
                               VALUES(@TypeId,@HbName,@HbDesc,@ImageUrl)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@TypeId", DbType.Int32, entity.TypeId);
                db.AddInParameter(cmd, "@HbName", DbType.String, entity.HbName);
                db.AddInParameter(cmd, "@HbDesc", DbType.String, entity.HbDesc);
                db.AddInParameter(cmd, "@ImageUrl", DbType.AnsiString, entity.ImageUrl);
                var ds = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public int GetPageCount(string search)
        {
            var count = 0;
            try
            {
                string sql =string.Format( @"SELECT COUNT(Id) From DMS_Hobbites WHERE IsDelete=0 {0}", search);
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

        public List<Hobbites> GetAll()
        {
            var list = new List<Hobbites>();
            try
            {
                string sql = @"SELECT Id,TypeId,HbName,HbDesc,ImageUrl,[ModifyDate],CreateDate,IsDelete FROM DMS_Hobbites Where IsDelete=0";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new Hobbites()
                    {
                        Id = x.Field<Int64>("Id"),
                        TypeId = x.Field<int>("TypeId"),
                        HbName = x.Field<string>("HbName"),
                        HbDesc = x.Field<string>("HbDesc"),
                        ImageUrl = x.Field<string>("ImageUrl"),
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
    }
}
