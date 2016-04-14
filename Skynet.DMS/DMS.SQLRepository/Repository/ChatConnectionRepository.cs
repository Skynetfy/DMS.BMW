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
    public class ChatConnectionRepository : BaseRepository, IChatConnectionRepository
    {
        public ChatConnection Add(ChatConnection entity)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[DMS_Connection]([Uid],Connected,UserAgent,ConnectionId) VALUES(@Uid,@Connected,@UserAgent,@ConnectionId)";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Uid", DbType.Int64, entity.Uid);
                db.AddInParameter(cmd, "@Connected", DbType.Boolean, entity.Connected);
                db.AddInParameter(cmd, "@UserAgent", DbType.String, entity.UserAgent);
                db.AddInParameter(cmd, "@ConnectionId", DbType.String, entity.ConnectionId);
                var ds = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public List<ChatConnection> GetByUid(long uid)
        {
            var list = new List<ChatConnection>();
            try
            {
                var sql = @"SELECT * FROM [dbo].[DMS_Connection] WHERE [Uid]=@Uid AND Connected=1 ";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Uid", DbType.Int64, uid);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    list = dt.AsEnumerable().Select(x => new ChatConnection()
                    {
                        Id = x.Field<Int64>("Id"),
                        Uid = x.Field<Int64>("Uid"),
                        Connected = x.Field<bool>("Connected"),
                        UserAgent = x.Field<string>("UserAgent"),
                        ConnectionId = x.Field<string>("ConnectionId"),
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

        public int UpdateConnectioned(Int64 uid, string connectionid, bool bl)
        {
            var i = 0;
            try
            {
                var sql = @"UPDATE [dbo].[DMS_Connection] SET Connected=@Connected  WHERE [Uid]=@Uid AND ConnectionId=@ConnectionId ";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@Uid", DbType.Int64, uid);
                db.AddInParameter(cmd, "@ConnectionId", DbType.String, connectionid);
                db.AddInParameter(cmd, "@Connected", DbType.Boolean, bl);
                i = db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return i;
        }
    }
}
