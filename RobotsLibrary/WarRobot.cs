using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RobotsLibrary
{
    public class WarRobot : Robot, IWarRobot
    {
        private IWarRobot _robot;

        private WarRobot()
        {
            _robot =  (IWarRobot)typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
        }

        public void Shoot()
        {
            _robot.Shoot();
        }
    }
}
