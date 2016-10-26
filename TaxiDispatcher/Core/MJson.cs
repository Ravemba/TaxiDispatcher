using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.Core
{
    public class MJson : JObject
    {
        public new MJson Add(string key, JToken val) { base.Add(key, val); return this; }

        public new MJson Add(object cont) { base.Add(cont); return this; }
    }
}
