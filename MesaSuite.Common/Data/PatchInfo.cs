using System.Collections.Generic;

namespace MesaSuite.Common.Data
{
    public class PatchInfo
    {
        public long? PrimaryKey { get; set; }
        public string Method { get; set; }
        public Dictionary<string, object> Values { get; set; }
        public List<string> RequestFields { get; set; }
        public List<string> AdditionalFields { get; set; }
    }
}
