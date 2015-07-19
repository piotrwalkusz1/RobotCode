using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class WinCondition : MonoBehaviour
{
    public abstract bool IsConditionAchieved { get; }

    protected void CheckWinConditions()
    {
        MainController.CheckWinConditions();
    }
}
