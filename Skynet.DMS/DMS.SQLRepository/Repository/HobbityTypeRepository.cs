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
    public class HobbityTypeRepository:BaseRepository,IHobbityTypeRepository
    {
        public HobbityType Add(HobbityType entity)
        {
            try
            {
                string sql = @"INSERT INTO DMS_HobbityType(
                               TName,HbDesc)
                               VALUES(@TName,@HbDesc)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@TName", DbType.AnsiString, entity.TName);
                db.AddInParameter(cmd, "@HbDesc", DbType.AnsiString, entity.HbDesc);
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
                string sql = string.Format(@"SELECT COUNT(Id) From DMS_HobbityType WHERE IsDelete=0 {0}", search);
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
        public List<HobbityType> GetAll()
        {
            var list = new List<HobbityType>();
            try
            {
                string sql = @"SELECT Id,TName,HbDesc,IsDelete,[ModifyDate],CreateDate FROM DMS_HobbityType Where IsDelete=0";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new HobbityType()
                    {
                        Id = x.Field<Int64>("Id"),
                        TName = x.Field<string>("TName"),
                        HbDesc = x.Field<string>("HbDesc"),
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
