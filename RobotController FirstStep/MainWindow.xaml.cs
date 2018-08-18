using Microsoft.Kinect;
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
using Kinect.Toolbox;
using Coding4Fun.Kinect.Wpf;

namespace RobotController_FirstStep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor sensor;
        private SkeletonDisplayManager skeletonDisplayManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.KinectSensors.First();
            sensor.Start();
            sensor.ColorStream.Enable();
            sensor.DepthStream.Enable();
            sensor.SkeletonStream.Enable();
            Title = sensor.DeviceConnectionId;

            skeletonDisplayManager = new SkeletonDisplayManager(sensor, imageCanvas);

            sensor.AllFramesReady += sensor_AllFramesReady;
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (var frame = e.OpenColorImageFrame())
            {
                if (frame == null)
                    return; 

                imageCanvas.Background = new ImageBrush(frame.ToBitmapSource());
            }

            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                    return; 

                var skeletons = frame.GetSkeletons().Where(x => x.TrackingState == SkeletonTrackingState.Tracked).ToArray();
                skeletonDisplayManager.Draw(skeletons, false);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sensor != null && sensor.IsRunning == true)
            {
                sensor.Stop();
            }
        }
    }
}
