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


                    sensor.ColorStream.Enable(); //enable color camera
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

        private void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            // get single image frame from Kinect's camera
            using (var frame = sensor.ColorStream.OpenNextFrame(0))
            {
                // convert frame to bitmap
                var bitmap = CreateBitmapFromFrame(frame);

                // display bitmap on canvas
                imageCanvas.Background = new ImageBrush(bitmap);
            }
        }

        // Convert image frame taken from kinect to bitmap source
        private static BitmapSource CreateBitmapFromFrame(ColorImageFrame frame)
        {
            // Create buffer for image data
            var pixelData = new byte[frame.PixelDataLength];
            // copy image data from frame to buffer
            frame.CopyPixelDataTo(pixelData);

            // compute stride value -> size in bytes of every image line
            var stride = frame.Width * frame.BytesPerPixel;

            // create bitmap source, using 96dpi, rbg pixel format
            var bitmap = BitmapSource.Create(frame.Width, frame.Height, 96, 96, PixelFormats.Bgr32, null, pixelData, stride);
            return bitmap;
        }
    }
}
