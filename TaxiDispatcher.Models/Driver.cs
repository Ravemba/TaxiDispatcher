using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.Core;

namespace TaxiDispatcher.Models
{
    public class Driver: IEntity
    {
        public string Name { get; set; }
        public int Old { get; set; }

    }
}
