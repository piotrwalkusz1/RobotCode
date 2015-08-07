using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILoadGameButton : MonoBehaviour 
{
    private const string DISABLE_TEXT = "<puste>";

    public void LoadGame()
    {
        string playerName = GetComponentInChildren<Text>().text;

        MainController.LoadGame(playerName);
    }

    public void SetEnable(string text)
    {
        GetComponentInChildren<Text>().text = text;

        GetComponent<Image>().color = GUIController.Main._enableButtonColor;
    }

    public void SetDisable()
    {
        GetComponentInChildren<Text>().text = DISABLE_TEXT;

        GetComponent<Image>().color = GUIController.Main._disableButtonColor;
    }
}
