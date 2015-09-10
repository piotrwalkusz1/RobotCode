using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour
{
    public void LoadGame()
    {
        GUIController.Main.ShowLoadGamePanel();
    }

    public void Resume()
    {
        GUIController.Main.HideMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
