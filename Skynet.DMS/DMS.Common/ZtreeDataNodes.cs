using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DMS.Common
{
    public class ZtreeDataNodes
    {
        public int id { get; set; }

        public int pId { get; set; }

        public string pidStr { get; set; }

        public string gid { get; set; }

        public string name { get; set; }

        [JsonProperty(PropertyName = "checked", NullValueHandling = NullValueHandling.Ignore)]
        public bool Checked { get; set; }

        public bool open { get; set; }

        public string url { get; set; }

        public string target { get; set; }

        public string icon { get; set; }

        public string iconOpen { get; set; }

        public string iconClose { get; set; }

        public string iconSkin { get; set; }
    }
}
