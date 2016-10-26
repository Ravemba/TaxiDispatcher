using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.Models
{
    public class Order
    {
        public TrafficPoint[] TrafficPoints { get; set; }
        public string Client { get; set; }

        public double Distance { get; set; }
    }

    public class TrafficPoint
    {
        public City City { get; set; }
        public string Address { get; set; }
        public Location location { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public Location location { get; set; }
    }
}
