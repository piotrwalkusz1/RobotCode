using UnityEngine;
using System.Collections;

public class WinAreaTargetOneTime : WinCondition
{
    public override bool IsConditionAchieved
    {
        get { return _isConditionAchieved; }
    }

    private bool _isConditionAchieved;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RealRobot>() != null)
        {
            _isConditionAchieved = true;

            GUIController.ShowMessage("Punkt zaliczony!", MessageColor.Green);

            RefreshWinConditions();
        }
    }

    
}
