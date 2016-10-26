using MapControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.Models;

namespace TaxiDispatcher.Helpers
{
    public class DataProvider
    {
        public static async Task<List<SectorPolyline>> GetSectorList()
        {
            return await Task<List<SectorPolyline>>.Factory.StartNew(() => {
                return new List<SectorPolyline> {
                    new SectorPolyline
                        {
                            Locations = LocationCollection.Parse("48.46454,35.04720 48.46454,35.04820 48.46554,35.04820 48.46554,35.04720 48.46454,35.04720")
                        }
                };
            });
        }

        public static async Task<List<Point>> GetDriverList()
        {
            return await Task<List<Point>>.Factory.StartNew(() => {
                return new List<Point> {
                    new Point
                        {
                            Name = "Driver 1",
                            Location = new Location(48.46494, 35.04760)
                        }
                };
            });

        }
    }
}
