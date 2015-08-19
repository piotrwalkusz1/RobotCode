using UnityEngine;
using System.Collections;

public class GUIDocNavButton : MonoBehaviour
{
    public void Click()
    {
        GUIDocumentation.Main.ShowDocText(GetComponent<Identificator>()._id);
    }
}
