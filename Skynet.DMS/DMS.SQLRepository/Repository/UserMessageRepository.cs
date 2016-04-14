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
    public class UserMessageRepository : BaseRepository, IUserMessageRepository
    {
        public UserMessage Add(UserMessage entity)
        {
            return new UserMessage();
        }

        public int UpdateStatus(Int64 id)
        {
            int i = 0;
            try
            {
                var sql = @"UPDATE DMS_UserMessage SET [Status]=1 WHERE Id=@Id";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Id", DbType.Int64, id);
                i = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return i;
        }

        public List<UserMessage> GetMessages(long toUid, int status, MessageTypeEnum type)
        {
            var list = new List<UserMessage>();
            try
            {
                var sql = @"SELECT Id,[Uid],[Type],ToUid,Content,[Status],[ModifyDate],CreateDate
                          FROM DMS_UserMessage WHERE ToUid=@ToUid AND [Status]=@Status AND Type=@Type AND IsDelete=0";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@ToUid", DbType.Int64, toUid);
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
                db.AddInParameter(cmd, "@Type", DbType.String, type.ToString());
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new UserMessage()
                    {
                        Id=x.Field<long>("Id"),
                        Uid = x.Field<long>("Uid"),
                        Type = x.Field<MessageTypeEnum>("Type"),
                        ToUid = x.Field<long>("ToUid"),
                        Content = x.Field<string>("Content"),
                        Status = x.Field<int>("Status"),
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
