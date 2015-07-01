using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    public static GUIController Main { get; set; }

    public List<MonoBehaviour> _componentsDisableWhileCodeEditor;
    public GameObject _codeEditor;

    void Awake()
    {
        if (Main == null)
        {
            Main = this;
        }
        else
        {
            print("There are two GUIControllers at least.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(InputSettings.Main._openCodeEditor))
        {
            _codeEditor.SetActive(!_codeEditor.activeSelf);

            _componentsDisableWhileCodeEditor.ForEach(x => x.enabled = !_codeEditor.activeInHierarchy);
        }
    }
}
