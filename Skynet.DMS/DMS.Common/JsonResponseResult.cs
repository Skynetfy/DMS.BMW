using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common
{
    public class JsonResponseResult<T> where T : class
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public List<T> Data { get; set; }

        public T Result { get; set; }
    }
}
