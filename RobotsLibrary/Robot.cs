using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public class Robot
    {
        private IRobot _robot;

        public void SetMovementSpeed(float speed)
        {
            _robot.SetMovementSpeed(speed);
        }

        public float GetMaxMovementSpeed()
        {
            return _robot.GetMaxMovementSpeed();
        }

        public void Move(float distance)
        {
            _robot.Move(distance);
        }

        public void StartMove(float time)
        {
            _robot.StartMove(time);
        }

        public void SetRotationSpeed(float speed)
        {
            _robot.SetRotationSpeed(speed);
        }

        public float GetMaxRotationSpeed()
        {
            return _robot.GetMaxRotationSpeed();
        }

        public void Rotate(float deegres)
        {
            _robot.Rotate(deegres);
        }

        public void StartRotate(float time)
        {
            _robot.StartRotate(time);
        }

        public void Wait(float seconds)
        {
            _robot.Wait(seconds);
        }

        public float Raycast()
        {
            return _robot.Raycast();
        }
        public float Raycast(float x, float y)
        {
            return _robot.Raycast(x, y);
        }
    }
}
