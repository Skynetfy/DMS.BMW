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
    public class ChatUserRepository:BaseRepository,IChatUserRepository
    {
        public ChatUser Add(ChatUser entity)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[DMS_ChatUser](UserName,ConnectionIds) VALUES(@UserName,@ConnectionIds);SELECT SCOPE_IDENTITY()";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@UserName", DbType.String, entity.UserName);
                db.AddInParameter(cmd, "@ConnectionIds", DbType.String, entity.ConnectionIds);
                var ds = db.ExecuteScalar(cmd);
                entity.Id = (long) ds;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return entity;
        }

        public ChatUser GetByName(string userName)
        {
            ChatUser user = null;
            try
            {
                var sql = @"SELECT TOP 1 * FROM [dbo].[DMS_ChatUser] WHERE [UserName]=@UserName ";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@UserName", DbType.String, userName);
                var ds = db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var dt = ds.Tables[0];
                    user.Id = Convert.ToInt64(dt.Rows[0]["Id"]);
                    user.UserName = dt.Rows[0]["UserName"].ToString();
                    user.ConnectionIds = dt.Rows[0]["ConnectionIds"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
            return user;
        }
    }
}
