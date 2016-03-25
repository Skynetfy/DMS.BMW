using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.MongoDB
{
    public static class NlogExtension
    {
        public static void InfoWithPushToken(this ILogger logger, string message, string pushToken)
        {

            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, logger.Name, message);
            theEvent.Properties["PushToken"] = pushToken;

            logger.Log(theEvent);
        }
    }
}
