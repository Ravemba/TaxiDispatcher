using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.Helpers
{
    interface IMapProvider
    {
        Location[] GetTrafficPoints();

    }
}
