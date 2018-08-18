using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace FirstConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (btnStartStop.Content.ToString() == "Start") // check if kinect is not connected yet
            {
                if (KinectSensor.KinectSensors.Count > 0) // check if kinect is connected to pc
                {
                    KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;

                    sensor = KinectSensor.KinectSensors.First(); // get and store kinect to property
                    sensor.Start(); // start connection
                    lblConnectionId.Content = sensor.DeviceConnectionId; // set display text of status label to connection id
                    btnStartStop.Content = "Stop"; // change display text of  start stop button
                }
            }
            else // if kinect is connected
            {
                if (sensor != null && sensor.IsRunning) // check if we have sensor and it is running
                {
                    sensor.Stop(); // disconect sensor
                    btnStartStop.Content = "Start"; // set button text to Start

                    lblStatus.Content = "-";
                    lblConnectionId.Content = "-";
                }
            }
        }

        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            lblStatus.Content = e.Status.ToString();
        }

        public KinectSensor sensor { get; set; }
    }
}
