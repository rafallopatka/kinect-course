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
using Coding4Fun.Kinect.Wpf;

namespace FirstConnection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public KinectSensor sensor { get; set; }

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

                    sensor.ColorStream.Enable();
                    //sensor.ColorFrameReady += sensor_ColorFrameReady;

                    sensor.DepthStream.Enable();
                    sensor.DepthFrameReady += sensor_DepthFrameReady;
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

        void sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenDepthImageFrame())
            {
                if (frame != null)
                {
                    this.imageCanvas.Background = new ImageBrush(frame.ToBitmapSource());
                }
            }
        }

        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            lblStatus.Content = e.Status.ToString();
        }

        private BitmapSource CreateBitmapFromFrame(ColorImageFrame frame)
        {
            var pixelData = new byte[frame.PixelDataLength];
            frame.CopyPixelDataTo(pixelData);
            var stride = frame.Width * frame.BytesPerPixel;

            ConverToGrayScale(pixelData);
            var bitmap = BitmapSource.Create(frame.Width, frame.Height, 96, 96, PixelFormats.Bgr32, null, pixelData, stride);
            return bitmap;
        }

        void sensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenColorImageFrame())
            {
                if (frame != null)
                {
                    var bitmap = CreateBitmapFromFrame(frame);
                    imageCanvas.Background = new ImageBrush(bitmap);

                    //imageCanvas.Background = new ImageBrush(frame.ToBitmapSource()); 
                }
            }
        }

        public void ConverToGrayScale(byte[] pixelData)
        {
            //For every pixel in the image 
            //Number 4 is used because every pixel is stored into 4 bytes in memmory
            for (int i = 0; i < pixelData.Length; i+= 4) 
            {
                //find maximum value of R G B
                var max = Math.Max(pixelData[i], Math.Max(pixelData[i + 1], pixelData[i + 2]));

                // use maximum value to set r g b values.
                pixelData[i] = max;//blue
                pixelData[i+1] = max;//green
                pixelData[i+2] = max;//red
            }
        }

        private void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            using (var frame = sensor.ColorStream.OpenNextFrame(0))
            {
                var bitmap = CreateBitmapFromFrame(frame);
                imageCanvas.Background = new ImageBrush(bitmap);

                imageCanvas.Background = new ImageBrush(frame.ToBitmapSource());
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // check if we have sensor and it is running
            if (sensor != null && sensor.IsRunning) 
            {
                // disconect sensor
                sensor.Stop(); 
            }
        }
    }
}
