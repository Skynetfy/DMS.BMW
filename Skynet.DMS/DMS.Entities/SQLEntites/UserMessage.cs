using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Entities.SQLEntites
{
    public class UserMessage : BaseEntity
    {
        public long Uid { get; set; }

        public MessageTypeEnum Type { get; set; }

        public long ToUid { get; set; }

        public int Status { get; set; }

        public string Content { get; set; }
    }

    public enum MessageTypeEnum
    {
        提醒消息 = 1
    }
}
