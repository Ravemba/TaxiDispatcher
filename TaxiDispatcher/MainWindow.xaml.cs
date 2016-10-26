using MapControl;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaxiDispatcher.Models;
using TaxiDispatcher.ViewModels;

namespace TaxiDispatcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mVM;
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            mVM = (MainWindowViewModel)DataContext;
            map.ZoomLevel = 16;
        }

        private void MapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                map.ZoomMap(e.GetPosition(map), Math.Floor(map.ZoomLevel + 1.5));
                //map.TargetCenter = map.ViewportPointToLocation(e.GetPosition(map));
            }

            if (e.ClickCount == 1 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                MessageBox.Show("Control key was held down.");
                //map.TargetCenter = map.ViewportPointToLocation(e.GetPosition(map));
            }
        }

        private void MapMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                map.ZoomMap(e.GetPosition(map), Math.Ceiling(map.ZoomLevel - 1.5));
            }
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            var location = map.ViewportPointToLocation(e.GetPosition(map));
            var latitude = (int)Math.Round(location.Latitude * 60000d);
            var longitude = (int)Math.Round(Location.NormalizeLongitude(location.Longitude) * 60000d);
            var latHemisphere = 'N';
            var lonHemisphere = 'E';

            if (latitude < 0)
            {
                latitude = -latitude;
                latHemisphere = 'S';
            }

            if (longitude < 0)
            {
                longitude = -longitude;
                lonHemisphere = 'W';
            }

            mouseLocation.Text = string.Format(CultureInfo.InvariantCulture,
                "{0}  {1:00} {2:00.000}\n{3} {4:000} {5:00.000}",
                latHemisphere, latitude / 60000, (double)(latitude % 60000) / 1000d,
                lonHemisphere, longitude / 60000, (double)(longitude % 60000) / 1000d);
        }

        private void MapMouseLeave(object sender, MouseEventArgs e)
        {
            mouseLocation.Text = string.Empty;
        }

        private void MapManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 0.001;
        }

        private void MapItemTouchDown(object sender, TouchEventArgs e)
        {
            var mapItem = (MapItem)sender;
            mapItem.IsSelected = !mapItem.IsSelected;
            e.Handled = true;
        }

        private void MapItemsControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(mVM.DrvLineName);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (var item in e.RemovedItems)
            {
                if (mVM.SelectedOrders.Contains((Order)item))
                {
                    mVM.SelectedOrders.Remove((Order)item);
                }
            }

            foreach (var item in e.AddedItems)
            {
                if (!mVM.SelectedOrders.Contains((Order)item))
                {
                    mVM.SelectedOrders.Add((Order)item);
                }
            }
            mVM.ShownOrderTraficLine(mVM.SelectedOrders);
        }

        Timer timer1;
        private void MapItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            var iSour = ((MapItemsControl)sender).ItemsSource;
            var en = iSour.GetEnumerator();
            en.MoveNext();
            mVM.OrderInfoOnLine.Add(new Models.Point() { Name="Hello", Location = ((SectorPolyline)en.Current).Locations[0] });
            //timer1 = new Timer();
            //timer1.Enabled = true;
            //timer1.Interval = 15000;
            //timer1.Elapsed += close_alert;
            //timer1.Start();
        }
        private void close_alert(object sender, EventArgs e)
        {
            //mVM.OrderInfoOnLine.Clear();
            timer1.Stop();
        }
    }
}
