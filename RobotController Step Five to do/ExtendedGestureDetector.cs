using Kinect.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotController_FirstStep
{
    public class ExtendedGestureDetector : SwipeGestureDetector
    {
        protected override void LookForGesture()
        {
            if (ScanPositions ((p1, p2) => Math.Abs(p2.Y - p1.Y) < 0.15f, 
               (p1, p2) => p2.Z - p1.Z <0.01f, 
               (p1, p2) => Math.Abs(p2.Z - p1.Z) > 0.2f, 250, 2500))
            {
                RaiseGestureDetected ("BackToFront");
                return;
            }

            if (ScanPositions ((p1, p2) => Math.Abs(p2.Y - p1.Y) < 0.15f, 
               (p1, p2) => p2.Z - p1.Z>-0.04f, 
               (p1, p2 ) => Math.Abs(p2.Z - p1.Z)> 0.4f, 250, 2500))
            {
                RaiseGestureDetected ("FrontToBack");
                return;
            }
        }
    }
}
