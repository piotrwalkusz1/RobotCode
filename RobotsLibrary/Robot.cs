using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public class Robot
    {
        private IRobot _robot;

        public void Move(float distance)
        {
            _robot.Move(distance);
        }

        public void Rotate(float deegres)
        {
            _robot.Rotate(deegres);
        }

        public void Wait(float seconds)
        {
            _robot.Wait(seconds);
        }
    }
}
