using UnityEngine;
using System.Collections;

public class InputSettings : MonoBehaviour
{
    public static InputSettings Main { get; set; }

    public KeyCode _openCodeEditor;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two InputSettings at least");
        }
    }
}
