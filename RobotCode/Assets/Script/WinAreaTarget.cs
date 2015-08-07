using UnityEngine;
using System.Collections;

public class WinAreaTarget : WinCondition
{
    public int _key;

    private bool _isConditionAchieved;

    public override bool IsConditionAchieved
    {
        get 
        {
            return _isConditionAchieved;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var parcel = other.GetComponent<WinAreaParcel>();

        if (parcel != null && parcel._key == this._key)
        {
            _isConditionAchieved = true;

            RefreshWinConditions();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var parcel = other.GetComponent<WinAreaParcel>();

        if (parcel != null && parcel._key == this._key)
        {
            _isConditionAchieved = false;

            RefreshWinConditions();
        }
    }
}
