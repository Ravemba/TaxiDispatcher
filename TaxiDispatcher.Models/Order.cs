using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.Core;

namespace TaxiDispatcher.Models
{
    public class Order : IEntity
    {
        public string Number { get; set; }
    }
}
