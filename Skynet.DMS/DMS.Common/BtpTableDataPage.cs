using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common
{
    public class BtpTableDataPage<T> where T : class
    {
        public long total { get; set; }
        public IEnumerable<T> rows { get; set; }
    }
}
