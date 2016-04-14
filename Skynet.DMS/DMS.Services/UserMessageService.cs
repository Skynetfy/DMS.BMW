using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;
using DMS.SQLRepository;

namespace DMS.Services
{
    public class UserMessageService
    {
        private static IUserMessageRepository userMessageRepository = DbRepositoryFactory.DbUserMessageRepository;

        public static void AddUserMessage(UserMessage entity)
        {
            userMessageRepository.Add(entity);
        }

        public static List<UserMessage> GetUserMessages(long toUid, int status, MessageTypeEnum type)
        {
            return userMessageRepository.GetMessages(toUid, status, type);
        }
    }
}
