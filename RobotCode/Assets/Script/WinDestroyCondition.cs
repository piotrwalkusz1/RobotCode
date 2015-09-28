using UnityEngine;
using System.Collections;

public class WinDestroyCondition : WinCondition
{
    public void Start()
    {
        GetComponent<BulletTarget>().OnDestroy += ActionOnDestroy;
    }

    public override bool IsConditionAchieved
    {
        get { return _isConditionAchieved; }
    }

    private bool _isConditionAchieved = false;

    public void ActionOnDestroy()
    {
        GUIController.ShowMessage("Obiekt został zniszczony!", MessageColor.Green);

        _isConditionAchieved = true;

        RefreshWinConditions();
    }
}
