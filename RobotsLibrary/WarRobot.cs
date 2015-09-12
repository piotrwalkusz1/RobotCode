using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RobotsLibrary
{
    public class WarRobot : Robot, IWarRobot
    {
        private IWarRobot _warRobot;

        public void Shoot()
        {
            CheckWarRobotVariable();

            _warRobot.Shoot();
        }

        private void CheckWarRobotVariable()
        {
            if (_warRobot == null)
                _warRobot = (IWarRobot)typeof(Robot).GetField("_robot", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
        }
    }
}
