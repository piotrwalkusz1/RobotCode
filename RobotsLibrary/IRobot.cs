using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public interface IRobot
    {
        void SetMovementSpeed(float speed);
        float GetMaxMovementSpeed();
        void Move(float distance);
        void StartMove(float time);
        void SetRotationSpeed(float speed);
        float GetMaxRotationSpeed();
        void Rotate(float deegres);
        void StartRotate(float time);
        void Wait(float seconds);
        float Raycast();
        float Raycast(float x, float y);
        void Light();
    }
}
