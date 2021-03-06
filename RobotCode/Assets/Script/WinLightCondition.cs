﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WinLightCondition : WinCondition
{
    private RealRobot _realRobot;

    public override bool IsConditionAchieved
    {
        get { return _isConditionAchieved; }
    }

    private bool _isConditionAchieved;

    void Awake()
    {
        _realRobot = GetComponent<RealRobot>();

        _realRobot.OnLightFunction += delegate()
        {
            GUIController.ShowMessage("Światło zostało włączone!", MessageColor.Green);

            _isConditionAchieved = true;

            RefreshWinConditions();
        };
    }
}
