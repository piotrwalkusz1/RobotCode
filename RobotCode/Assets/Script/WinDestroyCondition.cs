using UnityEngine;
using System.Collections;

public class WinDestroyCondition : WinCondition
{
    public override bool IsConditionAchieved
    {
        get { return _isConditionAchieved; }
    }

    private bool _isConditionAchieved = false;

    public void OnDestroy()
    {
        _isConditionAchieved = true;

        RefreshWinConditions();
    }
}
