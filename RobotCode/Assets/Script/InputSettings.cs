using UnityEngine;
using System.Collections;

public class InputSettings : MonoBehaviour
{
    public static InputSettings Main { get; set; }

    public KeyCode _openMenu;
    public KeyCode _openCodeEditor;
    public KeyCode _openDocumentation;

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

    void Update()
    {
        if (Input.GetKeyDown(_openDocumentation) && !GUIController.Main._codeEditor.gameObject.activeInHierarchy && MainController.IsGame)
        {
            if (GUIController.Main._documentation.gameObject.activeInHierarchy)
            {
                GUIController.HideDocumentation();
            }
            else
            {
                GUIController.ShowDocumentation();

                GUIDocumentation.Main.LoadDocumentation();
            }         
        }

        if (Input.GetKeyDown(_openMenu) &&  MainController.IsGame)
        {
            if (GUIController.Main._menu.gameObject.activeInHierarchy)
            {
                GUIController.Main.HideMenu();
            }
            else
            {
                GUIController.ShowMenu();
            }
        }
    }
}
