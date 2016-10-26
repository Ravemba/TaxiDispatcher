using MapControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaxiDispatcher.Core;
using TaxiDispatcher.Helpers;
using TaxiDispatcher.Models;

namespace TaxiDispatcher.ViewModels
{
    internal class MainWindowViewModel : NotifyControl
    {
        //private List<SectorPolyline> mSectors;

        public ObservableCollection<SectorPolyline> Sectors { get; set; }
        public ObservableCollection<Point> DriverPoints { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<SectorPolyline> DrvLine { get; set; }
        public ObservableCollection<Point> OrderInfoOnLine { get; set; }

        //private ObservableCollection<Order> _selectedOrders;
        public ObservableCollection<Order> SelectedOrders { get; set; }
        //{
        //    get { return _selectedOrders; }
        //    set
        //    {
        //        if (_selectedOrders == value) return;
        //        _selectedOrders = value;
        //        ShownOrderTraficLine(_selectedOrders);
        //        //OnPropertyChanged();
        //    }
        //}

        private string _drvLineName;
        public string DrvLineName
        {
            get { return _drvLineName; }
            set
            {
                if (_drvLineName == value) return;
                _drvLineName = value;
                OnPropertyChanged();
            }
        }

        private Order _lastSelOrder;
        public Order LastSelOrder
        {
            get { return _lastSelOrder; }
            set
            {
                if (_lastSelOrder == value) return;
                _lastSelOrder = value;
                OnPropertyChanged();
            }
        }



        public void ShownOrderTraficLine(ObservableCollection<Order> _selectedOrders)
        {
            DrvLine.Clear();
            foreach (var _selectedOrder in _selectedOrders)
            {
                var res = OSMProvider.GetTrafficPoints(_selectedOrder.TrafficPoints[0].Address, _selectedOrder.TrafficPoints[_selectedOrder.TrafficPoints.Length - 1].Address);

                var resDist = OSMProvider.GetDistance(_selectedOrder.TrafficPoints[0].Address, _selectedOrder.TrafficPoints[_selectedOrder.TrafficPoints.Length - 1].Address);

                var aw = res.GetAwaiter();
                aw.OnCompleted(() =>
                {
                    var spl = new SectorPolyline();
                    spl.Locations = new LocationCollection();
                    var rs = aw.GetResult();
                    foreach (var item in rs)
                    {
                        spl.Locations.Add(item);
                    }
                    DrvLine.Add(spl);
                    DrvLineName = _selectedOrder.Client;

                    var awD = resDist.GetAwaiter();
                    awD.OnCompleted(() =>
                    {
                        _selectedOrder.Distance = awD.GetResult();
                        LastSelOrder = _selectedOrder;
                    });
                });

            }
        }

        public MainWindowViewModel() {
            Init();
        }

        private void Init() {
            MapCenter = new Location(48.46454, 35.04720);
            Sectors = new ObservableCollection<SectorPolyline>();
            DriverPoints = new ObservableCollection<Point>();
            DrvLine = new ObservableCollection<SectorPolyline>();
            SelectedOrders = new ObservableCollection<Order>();
            OrderInfoOnLine = new ObservableCollection<Point>();

            LoadData();
        }

        private void LoadData()
        {
            var awSectlist = DataProvider.GetSectorList().GetAwaiter();
            awSectlist.OnCompleted(() =>
            {
                var collection = awSectlist.GetResult();
                foreach (var item in collection) { Sectors.Add(item); }
            });

            var awPointlist = DataProvider.GetDriverList().GetAwaiter();
            awPointlist.OnCompleted(() =>
            {
                var collection = awPointlist.GetResult();
                foreach (var item in collection) { DriverPoints.Add(item); }
            });

            Orders = new ObservableCollection<Order>();
            Orders.Add(new Order()
            {
                Client = "Jon",
                TrafficPoints = new TrafficPoint[2] {
                    new TrafficPoint { Address = "Фучика, 14б" },
                    new TrafficPoint { Address = "Кирова, 59"  } }
            });
            Orders.Add(new Order()
            {
                Client = "Bill",
                TrafficPoints = new TrafficPoint[2] {
                    new TrafficPoint { Address = "Коммунаровская, 26" },
                    new TrafficPoint { Address = "Глинки, 2"  } }
            });
            Orders.Add(new Order()
            {
                Client = "Henry",
                TrafficPoints = new TrafficPoint[2] {
                    new TrafficPoint { Address = "Карла маркса, 1" },
                    new TrafficPoint { Address = "Глинки, 2"  } }
            });
        }

        private Location mapCenter;
        public Location MapCenter
        {
            get { return mapCenter; }
            set
            {
                if (mapCenter == value) return;
                mapCenter = value;
                OnPropertyChanged();
            }
        }

        //private RelayCommand _ReadFileCommand;
        //public ICommand ReadFile => _ReadFileCommand ?? (_ReadFileCommand = new RelayCommand(OnReadFile));

        //private void OnReadFile(object par)
        //{
        //}


        //string _FileSource = "";
        //public string FileSource
        //{
        //    get { return _FileSource; }
        //    set
        //    {
        //        if (_FileSource == value) return;
        //        _FileSource = value;
        //        OnPropertyChanged();
        //    }
        //}

    }
}
