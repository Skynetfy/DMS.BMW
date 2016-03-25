using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;
using HZ.HHub.Data;
using NLog;

namespace DMS.SQLRepository.Repository
{
    public class BaseRepository
    {
        const string ConnStr = "DefaultConnection";
        protected ILogger logger;
        protected Database db = DatabaseFactory.CreateDatabaseByLocalConfig(ConnStr);

        public BaseRepository()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
    }
}
