﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotsLibrary
{
    public interface IRobot
    {
        void SetMovementSpeed(float speed);
        float GetMaxMovementSpeed();
        float GetMovementSpeed();
        void Move(float distance);
        void StartMove(float time);
        void SetRotationSpeed(float speed);
        float GetMaxRotationSpeed();
        float GetRotationSpeed();
        void Rotate(float deegres);
        void StartRotate(float time);
        void Wait(float seconds);
        float Raycast();
        float Raycast(float x, float y);
        void Light();
        void Answer(float answer);
        bool IsButtonPressed(string buttonName);
        void Print(string message);
        Position GetPosition(string objectName);
        Robot GetRobot(string name);
        Tower GetTower(string name);
    }
}
