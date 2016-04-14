using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;
using DMS.SQLRepository;

namespace DMS.Services
{
    public class ChatService
    {
        private static IChatUserRepository chatUserRepository = DbRepositoryFactory.DbChatUserRepository;
        private static IChatConnectionRepository chatConnectionRepository = DbRepositoryFactory.DbChatConnectionRepository;

        public static ChatUser AddChatUser(ChatUser entity)
        {
            return chatUserRepository.Add(entity);
        }

        public static void AddChatConnection(ChatConnection entity)
        {
            chatConnectionRepository.Add(entity);
        }

        public static ChatUser GetChatUser(string username)
        {
            return chatUserRepository.GetByName(username);
        }

        public static List<ChatConnection> GetChatConnectionByUid(Int64 uid)
        {
            return chatConnectionRepository.GetByUid(uid);
        }

        public static int UpdateConnectioned(Int64 uid, string connectionid, bool bl)
        {
            return chatConnectionRepository.UpdateConnectioned(uid, connectionid, bl);
        }
    }
}
