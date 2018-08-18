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
using MSHTML;

namespace RobotController_FirstStep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor sensor;
        private SkeletonDisplayManager skeletonDisplayManager;
        private SwipeGestureDetector gestureDetector;
        private AlgorithmicPostureDetector postureDetector;
        private Joint mouseJoint;

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

            gestureDetector = new ExtendedGestureDetector();
            gestureDetector.DisplayCanvas = mouseCanvas;
            gestureDetector.DisplayColor = Colors.Red;

            postureDetector = new AlgorithmicPostureDetector();
            postureDetector.PostureDetected += postureDetector_PostureDetected;

            MouseController.Current.ClickGestureDetector = gestureDetector;
            MouseController.Current.ImpostorCanvas = mouseCanvas;

            sensor.AllFramesReady += sensor_AllFramesReady;
            gestureDetector.OnGestureDetected += gestureDetector_OnGestureDetected;
        }

        void postureDetector_PostureDetected(string posture)
        {

        }

        void gestureDetector_OnGestureDetected(string gesture)
        {

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

                var skeleton = skeletons.FirstOrDefault();
                if(skeleton != null)
                {
                    var handJoint = skeleton.Joints.First(x => x.JointType == JointType.HandRight);
                    gestureDetector.Add(handJoint.Position, sensor);
                    postureDetector.TrackPostures(skeleton);

                    mouseJoint = handJoint;

                    MouseController.Current.SetHandPosition(sensor, handJoint, skeleton);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sensor != null && sensor.IsRunning == true)
            {
                sensor.Stop();
            }
        }

        private void ClickWebSiteElement(string id)
        {
            var document = (HTMLDocument)webBrowser.Document;

            var eleement = document.getElementById(id);
            eleement.click();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
