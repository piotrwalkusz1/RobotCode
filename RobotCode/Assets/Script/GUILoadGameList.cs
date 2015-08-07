using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GUILoadGameList : MonoBehaviour
{
    public List<GUILoadGameButton> _buttons;

    public void Initialize()
    {
        string[] saves = SaveController.GetAllSaves();

        for (int i = 0; i < _buttons.Count; i++)
        {
            if (i < saves.Length)
            {
                _buttons[i].SetEnable(saves[i]);
            }
            else
            {
                _buttons[i].SetDisable();
            }
        }
    }
}
