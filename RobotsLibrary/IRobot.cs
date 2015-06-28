using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public interface IRobot
    {
        void Move(float distance);
        void Rotate(float deegres);
        void Wait(float seconds);
    }
}
