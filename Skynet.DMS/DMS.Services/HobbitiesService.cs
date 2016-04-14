using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Entities.SQLEntites;
using DMS.SQLRepository;
using DMS.SQLRepository.ServiceReference1;

namespace DMS.Services
{
    public class HobbitiesService
    {
        private static IHobbitesRepository dbHobbitesRepository = DbRepositoryFactory.DbHobbitesRepository;
        private static IHobbityTypeRepository dbHobbityTypeRepository = DbRepositoryFactory.DbHobbityTypeRepository;

        #region Hobbites
        public static void AddHobbites(Hobbites entity)
        {
            dbHobbitesRepository.Add(entity);
        }

        public static List<Hobbites> GetHobbitesesList()
        {
            return dbHobbitesRepository.GetAll();
        }

        public static int GetHobbitiesPageCount(string search)
        {
            var where = "";
            if (!string.IsNullOrEmpty(search))
            {
                where = string.Format(@" AND HbName LIKE @'%{0}%'", search.Trim());
            }
            return dbHobbitesRepository.GetPageCount(where);
        }

        #endregion

        #region HobbityType
        public static void AddHobbityType(HobbityType entity)
        {
            dbHobbityTypeRepository.Add(entity);
        }

        public static List<HobbityType> GetHobbityTypesList()
        {
            return dbHobbityTypeRepository.GetAll();
        }

        public static int GetHobbityTypePageCount(string search)
        {
            var where = "";
            if (!string.IsNullOrEmpty(search))
            {
                where = string.Format(@" AND TName LIKE @'%{0}%'", search.Trim());
            }
            return dbHobbityTypeRepository.GetPageCount(where);
        }
        #endregion
    }
}
