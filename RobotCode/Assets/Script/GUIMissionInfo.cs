using UnityEngine;
using System.Collections;

public class GUIMissionInfo : MonoBehaviour
{
    public void Show()
    {
        GUIController.ShowMissionInfo();
    }

    public void Hide()
    {
        GUIController.HideMissionInfo();
    }
}
