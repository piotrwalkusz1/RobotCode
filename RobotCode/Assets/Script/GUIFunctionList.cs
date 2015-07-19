using UnityEngine;
using System.Collections;

public class GUIFunctionList : MonoBehaviour
{
    public RobotFunctions RobotFunctions
    {
        get
        {
            return GUIController.RealRobot.GetComponent<RobotFunctions>();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ShowFunctionCodeEditor(int functionIndex)
    {
        GUIController.ShowCodeEditor(RobotFunctions.Functions[functionIndex]);
    }
}
