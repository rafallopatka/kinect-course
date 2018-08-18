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
        private DepthImagePixel[] depthImagePixels;
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
                    //sensor.DepthFrameReady += sensor_DepthFrameReady;

                    sensor.SkeletonStream.Enable();

                    sensor.AllFramesReady += sensor_AllFramesReady;
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

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            depthImagePixels = new DepthImagePixel[sensor.DepthStream.FramePixelDataLength];
            using (var frame = e.OpenDepthImageFrame())
            {
                if (frame == null)
                    return;
                frame.CopyDepthImagePixelDataTo(depthImagePixels);
            }

            using (var frame = e.OpenColorImageFrame())
            {
                if (frame == null)
                    return;
                var bitmap = CreateBitmapFromFrame(frame);
                imageCanvas.Background = new ImageBrush(bitmap);
            }
        }

        void sensor_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenDepthImageFrame())
            {
                if (frame != null)
                {
                    //create buffer for depth data
                    var depthImagePixels = new DepthImagePixel[sensor.DepthStream.FramePixelDataLength];
                    //copy data to buffer
                    frame.CopyDepthImagePixelDataTo(depthImagePixels);

                    // create buffer for color pixels data
                    var colorPixels = new byte[4 * sensor.DepthStream.FramePixelDataLength];
                    
                    //for every color pixel in buffer
                    for (int i = 0; i < colorPixels.Length; i+=4)
                    {
                        //check if current pixels belongs to player
                        if (depthImagePixels[i / 4].PlayerIndex != 0)
                        {
                            //set color of that pixel to green
                            colorPixels[i + 1] = 255;
                        }
                    }
                    //display color pixels on screen
                    imageCanvas.Background = new ImageBrush(colorPixels.ToBitmapSource(640, 400));
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
            var mapper = new CoordinateMapper(sensor);
            var depthPoints = new DepthImagePoint[640 * 480];

            // map depth and pixel data
            mapper.MapColorFrameToDepthFrame(
                ColorImageFormat.RgbResolution640x480Fps30, 
                DepthImageFormat.Resolution640x480Fps30, 
                depthImagePixels, 
                depthPoints);

            for (int i = 0; i < depthPoints.Length; i ++) 
            {
                var pixelDataIndex = i * 4;
                var point = depthPoints[i];

                // if pointer is further than 800 mm and is not recognized as a valid point
                if (point.Depth > 800 || KinectSensor.IsKnownPoint(point) == false)
                {                  
                    var max = Math.Max(pixelData[pixelDataIndex], Math.Max(pixelData[pixelDataIndex + 1], pixelData[pixelDataIndex + 2]));

                    // use maximum value to set r g b values.
                    pixelData[pixelDataIndex] = max;//blue
                    pixelData[pixelDataIndex + 1] = max;//green
                    pixelData[pixelDataIndex + 2] = max;//red
                }
            }
        }

        private void btnTakePicture_Click(object sender, RoutedEventArgs e)
        {
            using (var frame = sensor.ColorStream.OpenNextFrame(0))
            {
                var bitmap = CreateBitmapFromFrame(frame);
                imageCanvas.Background = new ImageBrush(bitmap);

               // imageCanvas.Background = new ImageBrush(frame.ToBitmapSource());
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
